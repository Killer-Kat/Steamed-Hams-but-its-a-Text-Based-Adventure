using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Loot")]
public class Loot : InputAction
{
    string combinedInputWords;
    bool containerFound = false;
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        combinedInputWords = "";
        Debug.Log(separatedInputWords.Length);
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

        for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
        {
            if (combinedInputWords == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].name.ToLower())
            {
                controller.playerInventory.Add(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i]);
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.RemoveAt(i);
                controller.LogStringWithReturn("You take the " + combinedInputWords);
                return;
                //Look this isnt going to work like it did in python becuase we aren't inputing directly into the terminal so we can't just use input() however
                //this is Unity! I think we should utilize some kind of simple menu or something, like after it finds the container it pops up a menu that displays the name and contents
                //and then asks which item you want to take (unless the container is empty or only has one item in which case it would take the item or tell you its empty
                //menu nav could either be via mouse or via keyboard, mouse would be easy to implement but keyboard would be more authentic.
            }
        }
    }


}
