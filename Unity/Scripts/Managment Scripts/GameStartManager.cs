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
    [SerializeField]
    private InteractableObject apron;
    public List<InteractableObject> fridgeList;
    [TextArea]
    [SerializeField]
    private string chalmersIntDesc;
    [SerializeField]
    private InteractableObject tv;
    [SerializeField]
    private InteractableObject portrait;
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

        controller.playerInventory.Add(apron); //Adds the white apron Seymour is wearing at the start to the player inventory.

        chalmers.description = chalmersIntDesc;
        chalmers.name = "Chalmers";
        tv.examineDescription = "A small square purple colored CRT TV, it's missing an antenna. It's currently off yet something about it seems rather odd...";
        Kitchen.description = "A small square teal colored kitchen with a window overlooking a nearby fast food resturant. Its obvious whover lives here is not a very good cook.";
        oven.examineDescription = "A cheap white oven with a 4 burner stove and a broken timer. It is currently on.";
        
    }
    public void Start()
    {
        UpdatePortrait();
    }
    public void UpdatePortrait()
    {
        int tallymarks = controller.persistentData.NumberOfTimeLoops;
        switch (tallymarks)
        {
            default:
                portrait.description = "There is a portrait of you on the wall here.";
                portrait.examineDescription = "A life sized portrait of you, the eyes make you feel uneasy, they stare into your soul. Trying to tell you something but you just won't listen, not this time.";
                break;
            case 1:
                portrait.description = "There is a familiar portrait of you on the wall here.";
                portrait.examineDescription = "A familair looking, life sized portrait of you, someone has cut a slash into the left eye.";
                break;
            case 2:
                portrait.examineDescription = "A familair looking, life sized portrait of you, someone has cut two slashes into the face";
                break;  
            case 3:
                portrait.examineDescription = "A familair looking, life sized portrait of you, someone has cut three slashes into the face, but why?";
                break;       
            case 4:
                portrait.examineDescription = "A familair looking, life sized portrait of you, concerningly someone has cut four slashes into the face.";
                break;       
            case 5:
                portrait.description = "There is a damaged portrait of you on the wall here.";
                portrait.examineDescription = "A familair looking, life sized portrait of you, someone has cut a set of 5 talley marks into the face, but why?";
                break;       
            case 6:
                portrait.examineDescription = "A familair looking, life sized portrait of you, there are six tally marks on your face.";
                break;
            case 7:
                portrait.examineDescription = "It's the life sized portrait of you, there are 7 tally marks on your face.";
                break;            
            case 8:
                portrait.examineDescription = "It's the life sized portrait of you, there are 8 tally marks on your face.";
                break;
            case 9:
                portrait.examineDescription = "It's the life sized portrait of you, there are 9 tally marks on your face.";
                break;
            case 10:
                portrait.description = "Someone has been cutting tally marks into the portrait of you on the wall here.";
                portrait.examineDescription = "It's the life sized portrait of you, there are two sets of 5 tally marks on the face.";
                break;
            case 11:
                portrait.examineDescription = "It's the life sized portrait of you, there are 11 tally marks on the face.";
                break;
            case 12:
                portrait.examineDescription = "It's the life sized portrait of you, there are 12 tally marks on the portrait.";
                break;
            case 13:
                portrait.examineDescription = "It's the life sized portrait of you, 13 tally marks. Quite the unlucky number.";
                break;
            case 14:
                portrait.examineDescription = "It's the life sized portrait of you, there are 14 tally marks on the portrait.";
                break;
            case 15:
                portrait.description = "The portrait on the wall is covered in tally marks.";
                portrait.examineDescription = "It's the life sized portrait of you, there are 15 tally marks on the portrait.";
                break;
            case 16:
                portrait.examineDescription = "It's a life sized portrait of someone. It's hard to tell who its a portrait of as there are 16 tally marks obscuring it.";
                break;
            case 17:
                portrait.examineDescription = "It's a life sized portrait of someone. It's hard to tell who its a portrait of as there are 17 tally marks obscuring it.";
                break;

        }
    }

}
