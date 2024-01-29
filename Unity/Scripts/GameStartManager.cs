using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameController controller;
    [SerializeField]
    private List<Room> masterRoomList; //List of all the rooms
    //Due to the way unity scriptable objects work they are won't be reset when we start the game again. This script fixes that.
    [SerializeField]
    private List<InteractableObject> masterContainerList; //This is not the most optimal way to handle this I am sure.
    [SerializeField]
    private List<Person> masterPersonList; //Keeping lists of people is often frowned upon. 
    [SerializeField]
    private Person chalmers;
    [SerializeField]
    private Room Kitchen;
    [SerializeField]
    private InteractableObject fridge;
    [SerializeField]
    private InteractableObject oven;
    public List<InteractableObject> fridgeList;
    [TextArea]
    [SerializeField]
    private string chalmersIntDesc;
    [SerializeField]
    private InteractableObject tv;
    void Awake()
    {
        for (int i = 0; i < masterRoomList.Count; i++)
        {
            masterRoomList[i].InteractableObjectsInRoom.Clear(); //clears out any old objects left in the room.
            masterRoomList[i].InteractableObjectsInRoom.AddRange(masterRoomList[i].objectItializationList); //In theory adds the content of the initial list to the active list

            masterRoomList[i].peopleInRoom.Clear();// clears out any old people left in the room, also works on young people
            masterRoomList[i].peopleInRoom.AddRange(masterRoomList[i].peopleItializationList);

            masterRoomList[i].exits.Clear();// clears out any old exits left in the room
            masterRoomList[i].exits.AddRange(masterRoomList[i].exitItializationList);

        }

        for (int j = 0; j < masterContainerList.Count; j++) //Remember you have to add things to the list or this does not work! //dont add the fridge tho
        {
            masterContainerList[j].contents.Clear();
            masterContainerList[j].contents.AddRange(masterContainerList[j].contentsItializationList);// see above
        }
        fridge.contents.Clear();
        int RandomNum = Random.Range(0, fridgeList.Count);
        fridge.contents.Add(fridgeList[RandomNum]);
        for (int k = 0; k < masterPersonList.Count; k++)
        {
            masterPersonList[k].currentDialogue = masterPersonList[k].intialDialogue; //Should set the actors current dialogue to their starting dialogue.
        }

        controller.dialogueController.DisplaySpeakerName();//This should make it so you cannot see the speaker name at start.

        chalmers.description = chalmersIntDesc;
        chalmers.name = "Chalmers";
        tv.examineDescription = "A small square purple colored CRT TV, it's missing an antenna. It's currently off yet something about it seems rather odd...";
        Kitchen.description = "A small square teal colored kitchen with a window overlooking a nearby fast food resturant. Its obvious whover lives here is not a very good cook.";
        oven.examineDescription = "A cheap white oven with a 4 burner stove and a broken timer. It is currently on.";

        //idea fridge has a random selection of contents every playthough
    }
  

}
