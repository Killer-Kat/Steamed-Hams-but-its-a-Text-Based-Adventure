using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Open")]

public class Open : InputAction
{
    
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length <= 1)
        {
            controller.LogStringWithReturn("Error, To use this command you need to say what you want to open.");
        }
        else if (separatedInputWords.Length == 2)
        {
            
            for (int i = 0; i < controller.playerInventory.Count; i++)
            {
                 
                if (separatedInputWords[1].ToLower() == controller.playerInventory[i].noun.ToLower())//noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
                {
                    if (controller.playerInventory[i].canBeOpened == true)
                    {
                        controller.HAMS.UseActionTree(controller.playerInventory[i].useAction);
                    }else if(controller.playerInventory[i].isContainer == true)
                    {
                        peekAtContents(controller.playerInventory[i]);
                    }

                    return;
                }
            }
            for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
            {
                if (separatedInputWords[1].ToLower() == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun.ToLower())//noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
                {
                    if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].canBeOpened == true)
                    {
                        controller.HAMS.UseActionTree(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].useAction);
                    }
                    else if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].isContainer == true)
                    {
                        peekAtContents(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i]);
                    }

                    return;
                }
            }
            controller.LogStringWithReturn(separatedInputWords[1] + " not found.");

        }
        else //Item to look at is multiple words long
        {
            string itemToFind = "";
            for (int i = 1; i < separatedInputWords.Length; i++)
            {
                itemToFind = itemToFind + " " + separatedInputWords[i];
            }
            itemToFind = itemToFind.Substring(1);
            //Debug.Log("use searching for: " + itemToFind);
            for (int i = 0; i < controller.playerInventory.Count; i++) //look in the player inv first
            {
                //Debug.Log("use command finds: " + controller.playerInventory[i].noun); 
                if (itemToFind.ToLower() == controller.playerInventory[i].noun.ToLower())
                {//remember to keep everything lowercase!

                    controller.HAMS.UseActionTree(controller.playerInventory[i].useAction);
                    return;
                }
            }
            for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)//Then check the room
            {
                if (itemToFind.ToLower() == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun.ToLower())//noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
                {
                    controller.HAMS.UseActionTree(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].useAction);

                    return;
                }
            }
            controller.LogStringWithReturn(itemToFind + " not found.");
        }
         void peekAtContents(InteractableObject container)
        {
            string contentsText = "You peek inside the " + container.noun + " and see ";

            if (container.contents.Count > 0)
            {
                Debug.Log($"container.contents.Count: {container.contents.Count}");
                for (int j = 0; j < container.contents.Count; j++)
                {
                    if (j != 0)
                    {
                        contentsText += ", " + container.contents[j].noun.ToLower();
                    }
                    else
                    {
                        contentsText += container.contents[j].noun.ToLower();
                    }
                }
            }
            else
            {
                contentsText += "It's empty.";
            }
            controller.LogStringWithReturn(contentsText);

        }

    }

    
}

   

