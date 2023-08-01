using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Verbose")] //Dont make more than one of these.
public class Verbose : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length <= 1)
        {
            controller.LogStringWithReturn("Toggles Verbose mode. Use: Verbose -on -off");
        }
        else
        {
            if (separatedInputWords[1].ToLower() == "on")
            {
                controller.LogStringWithReturn("Verbose mode on");
                if(controller.isVerbose == true)
                {
                    controller.ToggleVeryVerboseMode();
                } else { controller.isVerbose = true; }
            } else if(separatedInputWords[1].ToLower() == "off")
            {
                controller.LogStringWithReturn("Verbose mode off");
                if(controller.isVeryVerbose == true)
                {
                    controller.ToggleVeryVerboseMode();
                }
                controller.isVerbose = false;
            }
        }
    }
}
