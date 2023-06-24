using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Drop")]
public class Drop : InputAction
{
    string combinedInputWords;
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        combinedInputWords = "";
        
        if (separatedInputWords.Length > 1)
        {
            for (int i = 1; i < separatedInputWords.Length; i++)
            {
                combinedInputWords = combinedInputWords + " " + separatedInputWords[i];
            }
            combinedInputWords = combinedInputWords.Substring(1);
        }
        else
        {
            combinedInputWords = separatedInputWords[1];
        }
        for (int i = 0; i < controller.playerInventory.Count; i++)
        {
            if (combinedInputWords == controller.playerInventory[i].name.ToLower())
            {
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(controller.playerInventory[i]);
                controller.playerInventory.RemoveAt(i);
                controller.LogStringWithReturn("You drop the " + combinedInputWords);
                return;
            }
        }
        controller.LogStringWithReturn(combinedInputWords + " not found."); //placeholder change later 
    }
}
