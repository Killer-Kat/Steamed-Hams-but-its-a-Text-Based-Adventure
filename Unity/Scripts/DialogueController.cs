using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text DialogueDisplay; //Display box for the Dialogue text

    public Text SpeakerNameDisplay;

    public Button Option1Button;
    public Button Option2Button;
    public Button Option3Button;
    public Button Option4Button;

    public Button CloseButton; //Closes the dialogue window.

    public DialogueObject button1nextDialogue; //The next dialogue object in the dialogue tree for the 1st option is stored here 
    public DialogueObject button2nextDialogue;
    public DialogueObject button3nextDialogue;
    public DialogueObject button4nextDialogue;

    [SerializeField]
    public DialogueTree dialogueTree;

    public GameController controller; //Need a reference to the game controller so I can get the regular textbox
    void Start()
    {
        DialogueDisplay.enabled = false;
        Option1Button.gameObject.SetActive(false); //Have to use gameobject.setactive or otherwise it says on the screen.
        Option2Button.gameObject.SetActive(false);
        Option3Button.gameObject.SetActive(false);
        Option4Button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    
    public void ToggleDisplay()
    {
        DialogueDisplay.enabled = !DialogueDisplay.enabled;
        controller.displayText.enabled = !controller.displayText.enabled;

        controller.textInput.inputField.enabled = !controller.textInput.inputField.enabled;

        Option1Button.gameObject.SetActive(!Option1Button.gameObject.active); //Deprecated, need to fix but not high priority
        Option2Button.gameObject.SetActive(!Option2Button.gameObject.active);
        Option3Button.gameObject.SetActive(!Option3Button.gameObject.active);
        Option4Button.gameObject.SetActive(!Option4Button.gameObject.active);
        DisplaySpeakerName(); //This should clear the field
        CloseButton.gameObject.SetActive(false); //Make sure that ToggleDisplay is called before unpackFromDialogueObject otherwise it could overwrite the close button state. //This is so that the button does not stay on the screen when you exit the dialogue
    }

    public void DisplayOptions(string npcText,string option1, string option2, string option3 = "NA", string option4 = "NA") //NA is used to specify that this option does not exist, the least you can have is 2 options.
    {
        DialogueDisplay.text = "";//probably not needed because the next line would reset the text anyway
        DialogueDisplay.text = npcText + "\n1. " + option1;
        if(option1 != "NA")
        {
            DialogueDisplay.text = npcText + "\n1. " + option1;
        }
        else { DialogueDisplay.text = npcText; }
        if (option2 != "NA")
        {
            DialogueDisplay.text = DialogueDisplay.text + "\n2. " + option2;
        }
        if(option3 == null)
        {
            //do nothing (yet)
        }
        else if(option3 != "NA")
        {
            DialogueDisplay.text = DialogueDisplay.text + "\n3. " + option3;
        }
        if(option4 == null)
        {
            //Subscribe to the CyberkatCafe podcast!
        }
        else if (option4 != "NA")
        {
            DialogueDisplay.text = DialogueDisplay.text + "\n4. " + option4;
        }
        
    }
    public void StartDialogue(DialogueObject Dobj,string npcName = "") //NPCs will call this method to bring up the dialogue system
    {
        ToggleDisplay();
        Debug.Log("Npc name to start dialogue" + npcName);
        DisplaySpeakerName(npcName);
        UnpackFromDialogueObject(Dobj);

    }
    public void UnpackFromDialogueObject(DialogueObject dObject)// call this with a dialogue object to put it into the dialogue system.
    {
        if(dObject.HAMScommand != "")
        {
            controller.HAMS.TakeInputFromDialogue(dObject.HAMScommand);
        }
        DisplayOptions(dObject.NpcDialogue, dObject.option1,dObject.option2,dObject.option3,dObject.option4);
        //Debug.Log(dialogueTree.testDialogue.NextDialogue1);
       // Debug.Log(dObject.NextDialogue1);
        SetupButtons(dObject.NextDialogue1, dObject.NextDialogue2, dObject.NextDialogue3, dObject.NextDialogue4);
        controller.updateScore(dObject.scoreToGive);

        if(dObject.IsEndOfDialogue == true)
        {
            CloseButton.gameObject.SetActive(true);
        }
        else { CloseButton.gameObject.SetActive(false); }

    }
    public void DisplaySpeakerName(string npcNameToDisplay = "")
    {
        Debug.Log("Npc name to start dialogue in display script " + npcNameToDisplay);
        SpeakerNameDisplay.text = npcNameToDisplay;
    }
    public void SetupButtons(DialogueObject b1, DialogueObject b2, DialogueObject b3, DialogueObject b4)
    {
        button1nextDialogue = b1;
        //Debug.Log("b1 " + b1);
        if(button1nextDialogue == null)
        {
            Option1Button.interactable = false; //In theory this should never happen, but thats just a theroy A GAME THEORY.
        } else { Option1Button.interactable = true; }
        button2nextDialogue = b2;
        if(button2nextDialogue == null)
        {
            Option2Button.interactable = false;
        }
        else { Option2Button.interactable = true; }
        button3nextDialogue = b3;
        if(button3nextDialogue == null)
        {
            Option3Button.interactable = false;
        } else { Option3Button.interactable = true; }
        button4nextDialogue = b4;
        if(button4nextDialogue == null)
        {
            Option4Button.interactable = false;
        }
        else { Option4Button.interactable = true; }
    }
    public void Button1()
    {
        UnpackFromDialogueObject(button1nextDialogue);
    }
    public void Button2()
    {
        UnpackFromDialogueObject(button2nextDialogue);
    }
    public void Button3()
    {
        UnpackFromDialogueObject(button3nextDialogue);
    }
    public void Button4()
    {
        UnpackFromDialogueObject(button4nextDialogue);
    }
}
