using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Debug")]
public class DebugCommand : InputAction //Cant name it debug becuase unity already has a debug script in this namespace and it will override it.
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        string x = separatedInputWords[1]; //This throws an out of range error if there is no second word. I tried to handle the exceptions and it did not work, so just don't do that.
            switch (x)
            {
                default:
                    controller.LogStringWithReturn(x + " Debug Command Not Found.");
                    break;
                case "clear":
                    controller.ClearTextbox();
                    break;
                case "dtoggle":
                    controller.dialogueController.ToggleDisplay();
                    break;
            case "dtest":
                controller.dialogueController.DisplayOptions("Manual input still works! why have this feature? idk.","test 1", "test 2");
                break;
            case "dtest2": //D test lol, I detest the word dialogue, why the ue seriously!
               DialogueObject dobj = controller.dialogueController.dialogueTree.testDialogue;
                controller.dialogueController.StartDialogue(dobj, "Mysteroius Voice");
                break;
            case "dtest3":
                controller.dialogueController.UnpackFromDialogueObject(controller.dialogueController.dialogueTree.testDialogue2);
                break;
            case "intro":
                controller.HAMS.IntroScene();
                break;
            case "refreshrm":
                controller.DisplayRoomText();
                break;
            case "goodbye":
                controller.HAMS.chalmersGoodbye();
                break;
            case "endgame":
                controller.ShowEndGamePopup(controller.score,controller.oddPoints,controller.politePoints);
                break;
            case "kitchenfire":
                controller.HAMS.KitchenOnFire();
                break;
            case "cek":
                controller.HAMS.ChalmersEntersKitchen();
                break;
            }
        
        
    }
}
