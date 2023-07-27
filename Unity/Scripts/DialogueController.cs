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
}
