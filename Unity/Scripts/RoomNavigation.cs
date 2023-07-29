using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom; //Sorry we don't offer private rooms here.


    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>(); //A dictionary that stores all the exits in the current room, I think its kinda stupid not to just look at the room instance but whatever
    private GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

    public void UnpackItemsInRoom()
    {
        for (int i = 0; i < currentRoom.InteractableObjectsInRoom.Count; i++)
        {
            controller.interactionDescriptionsInRoom.Add(currentRoom.InteractableObjectsInRoom[i].description);
        }
    }

    public void UnpackPeopleInRoom() //Warning do not try this at home, people do NOT apreciate being unpacked.
    {
        for (int i = 0; i < currentRoom.peopleInRoom.Count; i++)
        {
            controller.interactionDescriptionsInRoom.Add(currentRoom.peopleInRoom[i].description);
        }
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        if (exitDictionary.ContainsKey(directionNoun))
        {
            currentRoom = exitDictionary[directionNoun];
            controller.LogStringWithReturn("You head off to the " + directionNoun);
            controller.DisplayRoomText();
        }
        else
        {
            controller.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }
}
