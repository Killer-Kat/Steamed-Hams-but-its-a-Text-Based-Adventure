using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Place")]
public class Place : InputAction //put items in containers. place x in y
{
    // Start is called before the first frame update, not RespondToInput though, thats called when there is input from the text box.
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        bool isPrepositionFound = false;
        string preposition = "";

        for (int i = 0; i < separatedInputWords.Length; i++)
        {
            if (separatedInputWords[i] == "in" || separatedInputWords[i] == "on")
            {
                isPrepositionFound = true;
                preposition = separatedInputWords[i];
                break;
            }
        }

        if (isPrepositionFound)
        {
            string itemToPlace = "";
            string containerToFill = "";
            int prepositionIndex = Array.IndexOf(separatedInputWords, preposition);

            if (prepositionIndex > 2)
            {
                for (int i = 1; i < prepositionIndex; i++)
                {
                    itemToPlace = itemToPlace + " " + separatedInputWords[i];
                }
                itemToPlace = itemToPlace.Substring(1);
                Debug.Log(itemToPlace);
            }
            else
            {
                itemToPlace = separatedInputWords[1];
            }

            for (int i = prepositionIndex + 1; i < separatedInputWords.Length; i++)
            {
                containerToFill = containerToFill + " " + separatedInputWords[i];
            }
            
            containerToFill = containerToFill.Substring(1);
            //Debug.Log(containerToLoot); 
            for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
            {
                if (containerToFill == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].name.ToLower())
                {
                    //Debug.Log("Container found " + containerToLoot);
                    if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].isContainer == false)
                    {
                        controller.LogStringWithReturn(containerToFill + " is not a valid container.");
                        return;
                    }
                    for (int j = 0; j < controller.playerInventory.Count; j++)
                    {
                        if (itemToPlace == controller.playerInventory[j].name.ToLower())
                        {
                            controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].contents.Add(controller.playerInventory[j]);
                            controller.playerInventory.RemoveAt(j);
                            controller.LogStringWithReturn("You put the " + itemToPlace + " in the " + containerToFill);
                            return;
                        }
                    }
                    controller.LogStringWithReturn(itemToPlace + " not found in inventory.");
                    return;
                }
            }
            controller.LogStringWithReturn("Container " + containerToFill + " not found.");
            return;
        }
    }
}
