using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Eat")]
public class Eat : InputAction
{
    //The eat item logic will be handled in the HAMS script, same as Use
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length <= 1)
        {
            controller.LogStringWithReturn("Eat what?");
            return;
        }

        // Build item name (handles single or multi-word)
        string itemToFind = string.Join(" ", separatedInputWords[1..]).ToLower();

        // -----------------------------
        // First: look in player inventory
        // -----------------------------
        for (int i = 0; i < controller.playerInventory.Count; i++)
        {
            InteractableObject inventoryObject = controller.playerInventory[i];

            if (itemToFind == inventoryObject.noun.ToLower())
            {
                HandleEat(controller, inventoryObject);
                return;
            }
        }

        // -----------------------------
        // Second: look in the room
        // -----------------------------
        for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
        {
            InteractableObject roomObject = controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i];

            if (itemToFind == roomObject.noun.ToLower())
            {
                HandleEat(controller, roomObject);
                return;
            }
        }

        // -----------------------------
        // Alias fallback (inventory + room)
        // -----------------------------
        InteractableObject aliasObject = GetShortNameList(controller, itemToFind);

        if (aliasObject != null)
        {
            HandleEat(controller, aliasObject);
            return;
        }

        controller.LogStringWithReturn(itemToFind + " not found.");
    }

    // -----------------------------
    // Local helper: eat item
    // -----------------------------
    void HandleEat(GameController controller, InteractableObject interactableObject)
    {
        // If the item cannot be eaten, we shut that down immediately
        if (!interactableObject.canBeEaten)
        {
            controller.LogStringWithReturn("You can't eat the " + interactableObject.noun + ".");
            return;
        }

        // Trigger the HAMS eat logic
        controller.HAMS.EatActionTree(interactableObject.noun);

        // If the item is in the player's inventory, remove it after eating
        if (controller.playerInventory.Contains(interactableObject))
        {
            controller.playerInventory.Remove(interactableObject);
        }

        // If the item is in the room, remove it from the room
        if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Contains(interactableObject))
        {
            controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Remove(interactableObject);
        }

        
    }
}


