using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Drop")]
public class Drop : InputAction
{
    string combinedInputWords;

    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        // If the player typed "drop" and nothing else, we politely refuse to guess
        if (separatedInputWords.Length < 2)
        {
            controller.LogStringWithReturn("Drop what, exactly?");
            return;
        }

        // Normalize input so we stop caring about capitalization
        NormalizeInputWords(separatedInputWords);

        // Handle "drop all" before anything else
        if (separatedInputWords[1] == "all" || separatedInputWords[1] == "everything")
        {
            DropAll(controller);
            return;
        }

        // Build the item name. if you build it they will come
        combinedInputWords = string.Join(" ", separatedInputWords[1..]);

        // Try to drop by full name first
        var inventory = controller.playerInventory;

        for (int i = 0; i < inventory.Count; i++)
        {
            if (combinedInputWords == inventory[i].name.ToLower())
            {
                DropItem(controller, inventory[i]);
                return;
            }
        }

        // Full name failed — time to let the alias system save the day
        var aliasObj = GetShortNameList(controller, combinedInputWords);

        if (aliasObj != null && inventory.Contains(aliasObj))
        {
            DropItem(controller, aliasObj);
            return;
        }

        // If we got here, the player is trying to drop imaginary items
        controller.LogStringWithReturn(combinedInputWords + " not found.");
    }

    // -----------------------------
    // Helper: Normalize input words
    // -----------------------------
    void NormalizeInputWords(string[] words)
    {
        for (int i = 0; i < words.Length; i++)
            words[i] = words[i].ToLower();
    }

    // -----------------------------
    // Helper: Drop a single item
    // -----------------------------
    void DropItem(GameController controller, InteractableObject obj)
    {
        controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(obj);
        controller.playerInventory.Remove(obj);

        controller.LogStringWithReturn("You drop the " + obj.noun + ".");
    }

    // -----------------------------
    // Helper: Drop everything
    // -----------------------------
    void DropAll(GameController controller)
    {
        var inventory = controller.playerInventory;

        if (inventory.Count == 0)
        {
            controller.LogStringWithReturn("You have nothing to drop. Minimalism achieved.");
            return;
        }

        // Copy items so we don’t modify the list while iterating
        List<InteractableObject> toDrop = new List<InteractableObject>(inventory);

        foreach (var item in toDrop)
        {
            controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(item);
            inventory.Remove(item);
        }

        controller.LogStringWithReturn("You dramatically dump everything onto the floor.");
    }
}
