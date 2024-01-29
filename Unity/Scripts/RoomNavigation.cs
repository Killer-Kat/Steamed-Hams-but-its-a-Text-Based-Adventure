using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom; //Sorry we don't offer private rooms here.


    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>(); //A dictionary that stores all the exits in the current room, I think its kinda stupid not to just look at the room instance but whatever
    private GameController controller;

    public Exit Backrooms;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Count; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
        int randomNumber = Random.Range(0, 100);
        if(randomNumber == 3)
        {
            exitDictionary.Add(Backrooms.keyString, Backrooms.valueRoom);
            controller.interactionDescriptionsInRoom.Add(Backrooms.exitDescription);
        }
    }

    public void UnpackItemsInRoom()
    {
        for (int i = 0; i < currentRoom.InteractableObjectsInRoom.Count; i++)
        {
            controller.interactionDescriptionsInRoom.Add(currentRoom.InteractableObjectsInRoom[i].description);
            if (controller.isVerbose == true)
            {
                if (currentRoom.InteractableObjectsInRoom[i].isContainer == true)
                {
                    string contentsText = "It contains :";
                    for (int j = 0; j < currentRoom.InteractableObjectsInRoom[i].contents.Count; j++)
                    {
                        contentsText = contentsText + " " + currentRoom.InteractableObjectsInRoom[i].contents[j].noun.ToLower();
                    }
                    
                    controller.interactionDescriptionsInRoom.Add(contentsText);
                }
            }
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
