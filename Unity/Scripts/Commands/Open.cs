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
            return;
        }

        // Build item name (handles single or multi-word)
        string itemToFind = string.Join(" ", separatedInputWords[1..]).ToLower();

        // Try inventory first (full name)
        for (int i = 0; i < controller.playerInventory.Count; i++)
        {
            if (itemToFind == controller.playerInventory[i].noun.ToLower())
            {
                HandleOpen(controller, controller.playerInventory[i]);
                return;
            }
        }

        // Try room objects (full name)
        for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
        {
            InteractableObject roomObject = controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i];

            if (itemToFind == roomObject.noun.ToLower()) //noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
            {
                HandleOpen(controller, roomObject);
                return;
            }
        }

        // Alias fallback (inventory + room)
        InteractableObject aliasObject = GetShortNameList(controller, itemToFind);

        if (aliasObject != null)
        {
            HandleOpen(controller, aliasObject);
            return;
        }

        controller.LogStringWithReturn(itemToFind + " not found.");

        // -----------------------------
        // Local helper: open or peek
        // -----------------------------
        void HandleOpen(GameController gameController, InteractableObject interactableObject)
        {
            if (interactableObject.canBeOpened)
            {
                gameController.HAMS.UseActionTree(interactableObject.useAction);
            }
            else if (interactableObject.isContainer)
            {
                peekAtContents(interactableObject);
            }
            else
            {
                gameController.LogStringWithReturn("You can't open the " + interactableObject.noun + ".");
            }
        }

        // -----------------------------
        // Local helper: peek inside container
        // -----------------------------
        void peekAtContents(InteractableObject container)
        {
            string contentsText = "You peek inside the " + container.noun + " and see ";

            if (container.contents.Count > 0)
            {
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
