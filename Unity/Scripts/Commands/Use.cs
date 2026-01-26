using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Use")]
public class Use : InputAction
{

    //The use item tree containing the logic for all the different items is in the HAMS script
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length <= 1)
        {
            //Throw error
            controller.LogStringWithReturn("Use what?");
            return;
        }

        // Build item name (handles single or multi-word)
        string itemToFind = string.Join(" ", separatedInputWords[1..]).ToLower();

        //Debug.Log("Looking for: " + itemToFind);

        // -----------------------------
        // First: look in player inventory
        // -----------------------------
        for (int i = 0; i < controller.playerInventory.Count; i++)
        {
            if (itemToFind == controller.playerInventory[i].noun.ToLower())
            {
                controller.HAMS.UseActionTree(controller.playerInventory[i].useAction);
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
                controller.HAMS.UseActionTree(roomObject.useAction);
                return;
            }
        }

        // -----------------------------
        // Alias fallback (inventory + room)
        // -----------------------------
        InteractableObject aliasObject = GetShortNameList(controller, itemToFind);

        if (aliasObject != null)
        {
            controller.HAMS.UseActionTree(aliasObject.useAction);
            return;
        }

        // -----------------------------
        // Nothing found
        // -----------------------------
        controller.LogStringWithReturn(itemToFind + " not found.");
    }
}
