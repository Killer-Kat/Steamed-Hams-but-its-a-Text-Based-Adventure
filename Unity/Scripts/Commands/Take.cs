using System;
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
        //Debug.Log(separatedInputWords.Length);

        if(Array.Find(separatedInputWords, element => element == "from") == "from")
        {
            string itemToTake = "";
            string containerToLoot = "";
            int x = Array.IndexOf(separatedInputWords, "from");
            Debug.Log("from found at index " + x);
            if(x > 2)
            {
                for (int i = 1; i < x; i++)
                {
                    itemToTake = itemToTake + " " + separatedInputWords[i];
                }
                itemToTake = itemToTake.Substring(1);
                Debug.Log(itemToTake);

            }
            else
            {
                itemToTake = separatedInputWords[1];
            }
            for (int i = x + 1; i < separatedInputWords.Length; i++)
            {
                containerToLoot = containerToLoot + " " + separatedInputWords[i];
            }
            containerToLoot = containerToLoot.Substring(1);
            //Debug.Log(containerToLoot); 
            for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
            {
                if (containerToLoot == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].name.ToLower())
                {
                    //Debug.Log("Container found " + containerToLoot);
                    if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].isContainer == false)
                    {
                        controller.LogStringWithReturn(containerToLoot + " is not a valid container.");
                        return;
                    }
                    for (int j = 0; j < controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].contents.Count; j++)
                    {
                        if (itemToTake == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].contents[j].name.ToLower())
                        {
                            if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].contents[j].canTake == false)
                            {
                                controller.LogStringWithReturn("You cant take the " + controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].contents[j].name);
                                return;
                            }
                            controller.playerInventory.Add(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].contents[j]);
                            controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].contents.RemoveAt(j);
                            controller.LogStringWithReturn("You take the " + itemToTake + " from the " + containerToLoot);
                            return;
                        } 
                    }
                    controller.LogStringWithReturn(itemToTake + " not found in container " + containerToLoot);
                    return;
                }
            }
            controller.LogStringWithReturn("Container " + containerToLoot + " not found.");
            return;
        }

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
                if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].canTake == false)
                {
                    controller.LogStringWithReturn("You cant take the " + controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].name);
                    return;
                }
                controller.playerInventory.Add(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i]);
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.RemoveAt(i);
                controller.LogStringWithReturn("You take the " + combinedInputWords);
                return;
            } 
        }
        controller.LogStringWithReturn(combinedInputWords + " not found.");
    }
}
