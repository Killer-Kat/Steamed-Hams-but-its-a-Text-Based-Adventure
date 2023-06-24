using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Take")]
public class Take : InputAction
{
    string combinedInputWords;
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        combinedInputWords = "";
        Debug.Log(separatedInputWords.Length);
        if(separatedInputWords.Length > 1)
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
        for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
        {
            if(combinedInputWords == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].name.ToLower())
            {
                controller.playerInventory.Add(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i]);
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.RemoveAt(i);
                controller.LogStringWithReturn("You take the " + combinedInputWords);
                return;
            }
        }
        controller.LogStringWithReturn(combinedInputWords + " not found.");
    }
}
