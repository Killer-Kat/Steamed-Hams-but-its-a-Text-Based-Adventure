using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text displayText;
    public InputAction[] inputActions;
    public Text ScoreText;

    public TextInput textInput; //idk why I am linking everything here, mostly to keep everything in one place instead of having a dozen references in different scirpts.

    public bool isVerbose = false; //For verbose mode to be implemented later
    public bool isVeryVerbose = false; //IDEA!!! extremely verbose mode easter egg that includes a ton of mostly useless data, like internal stats, "moon phase", random stuff like that sort of like the undertale dating game

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    List<string> actionLog = new List<string>();
    public string combinedTextCache;

    public DialogueController dialogueController; //might not need this since the Dialogue objects are static now 

    //H.A.M.S Hastly Asembled Management Script
    public HAMS HAMS; //mmmmm steamed HAMS

    public List<InteractableObject> playerInventory;

    public int score;

    public int oddPoints;
    public int politePoints;

    public int secretNumber; //For use in easter eggs and also very verbose mode
    //Stuff for endgame UI
    public Text endGamePopupScoreText;
    public Text oddPointsText;
    public Text politePointsText;
    public Text creatorText; // For displaying the game creator's information
    public GameObject popupPanel;
    public void ShowEndGamePopup(int score, int oddPoints, int politePoints)
    {
        endGamePopupScoreText.text = "Game over you scored: " + score + " points.";
        oddPointsText.text = "Your personality was " + oddPoints + " Odd.";
        politePointsText.text = "Your personality was " + politePoints + " Polite.";
        creatorText.text = "Game by Killer Kat, if you liked this check out my other projects at cyberkatcafe.com";
        popupPanel.SetActive(true);
    }
    public void HideEndGamePopup()//Need to rename this
    {
        Application.Quit(); // Close the application when the function is called
    }
    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
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
        roomNavigation.UnpackPeopleInRoom();
    }

    
    void ClearCollectionsForNewRoom()
    {
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
        secretNumber = Random.Range(0, 100);
        //HAMS.Tick(); //For some reason I had this here? I moved it to Textinput InputComplete() so that its only triggered once for each command parsed. 
    }
    // Update is called once per frame
    public void updateScore(int x)
    {
        score = score + x;
        ScoreText.text = "Score: " + score;
    }

    public void UpdateOddPoints(int x)
    {
        oddPoints = oddPoints + x;
    }
    public void UpdatePolitePoints(int x)
    {
        politePoints = politePoints + x;
    }
  public void ToggleVeryVerboseMode()
    {
        isVeryVerbose = !isVeryVerbose;
    }
    public void ResetRoomExits(Room roomToReset)//Resets Room Exits, ok I know you didn't need this comment here to explain that, but then how else could I add this funny meta bit?
    {
        roomToReset.exits.Clear();
        roomToReset.exits.AddRange(roomToReset.exitItializationList);
        roomNavigation.UnpackExitsInRoom();
    }
}
