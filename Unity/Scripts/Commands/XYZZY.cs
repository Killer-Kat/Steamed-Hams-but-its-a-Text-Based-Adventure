using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/XYZZY")]
public class XYZZY : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        int magicNumber = Random.Range(0, 100);
        int evenMoreMagicNumber = Random.Range(0, controller.gameStartManager.masterRoomList.Count);
        if(magicNumber == 77)
        {
            controller.roomNavigation.currentRoom = controller.gameStartManager.masterRoomList[evenMoreMagicNumber];
            controller.LogStringWithReturn("With a flash of light you find yourself teleported to " + controller.roomNavigation.currentRoom.rooomName);
            controller.DisplayRoomText();
            controller.verboseSkip = true;
        }
        else
        {
            controller.LogStringWithReturn("A hollow voice says \"Nerd!\"");
        }
    }



}
