using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text DialogueDisplay; //Display box for the Dialogue text

    public Button Option1Button;
    public Button Option2Button;
    public Button Option3Button;
    public Button Option4Button;
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
       
    }

    public void DisplayOptions(string option1, string option2, string option3 = "NA", string option4 = "NA") //NA is used to specify that this option does not exist, the least you can have is 2 options.
    {
        DialogueDisplay.text = "";
        DialogueDisplay.text = "1. " + option1;
        DialogueDisplay.text = DialogueDisplay.text + "\n2. " + option2;
        if (option3 != "NA")
        {
            DialogueDisplay.text = DialogueDisplay.text + "\n3. " + option3;
        }
        if (option4 != "NA")
        {
            DialogueDisplay.text = DialogueDisplay.text + "\n4. " + option4;
        }
        
    }
    public void unpackFromDialogueObject(DialogueObject dObject)
    {
        DisplayOptions(dObject.option1,dObject.option2,dObject.option3,dObject.option4);
    }
}
