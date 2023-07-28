using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//IDEA!!! extremely verbose mode easter egg that includes a ton of mostly useless data, like internal stats, "moon phase", random stuff like that
public class GameController : MonoBehaviour
{
    public Text displayText;
    public InputAction[] inputActions;
    public Text ScoreText;

    public TextInput textInput; //idk why I am linking everything here, mostly to keep everything in one place instead of having a dozen references in different scirpts.

    public bool isVerbose = false; //For verbose mode to be implemented later

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    List<string> actionLog = new List<string>();

    public DialogueController dialogueController;


    public List<InteractableObject> playerInventory;

    public int score;
    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }



    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }
    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void ClearTextbox()//Clears textbox hopefully, This will be needed to implement more complex interactions later on.
    {
        actionLog.Clear(); //Clears the entire list of actions, There is probably a good reason not to do this. However there is no current way to navigate the log list and I dont ever use it for anything.
        Debug.Log("Display text cleared");
    }
    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();

        UnpackRoom();
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(roomNavigation.currentRoom.name + ":");
        LogStringWithReturn(combinedText);
    }

    private void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        roomNavigation.UnpackItemsInRoom();
    }

    
    void ClearCollectionsForNewRoom()
    {
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }
    // Update is called once per frame
    public void updateScore(int x)
    {
        score = score + x;
        ScoreText.text = "Score: " + score;
    }
    void Update()
    {
        
    }
}
