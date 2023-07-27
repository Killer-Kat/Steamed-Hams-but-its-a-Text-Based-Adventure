using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text DialogueDisplay; //Display box for the Dialogue text
    public GameController controller; //Need a reference to the game controller so I can get the regular textbox
    void Start()
    {
        DialogueDisplay.enabled = false;
    }

    // Update is called once per frame
    
    public void ToggleDisplay()
    {
        DialogueDisplay.enabled = !DialogueDisplay.enabled;
        controller.displayText.enabled = !controller.displayText.enabled;
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
}
