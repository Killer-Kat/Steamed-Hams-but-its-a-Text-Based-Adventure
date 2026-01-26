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
        // Normalize input so we stop caring about capitalization like it's the 1800s
        for (int i = 0; i < separatedInputWords.Length; i++)
            separatedInputWords[i] = separatedInputWords[i].ToLower();

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

        if (isPrepositionFound) //has to be, since we need to know where to place the item. Need to add an else that will tell the player the correct usage.
        {
            string itemToPlace = "";
            string containerToFill = "";
            int prepositionIndex = Array.IndexOf(separatedInputWords, preposition);

            // Handle "place all in X" before anything else
            if (separatedInputWords[1] == "all" || separatedInputWords[1] == "everything")
            {
                PlaceAllInContainer(controller, separatedInputWords, prepositionIndex);
                return;
            }

            if (prepositionIndex > 2) //check if the item to place is multiple words long
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

            // Try to find the container by full name first
            var roomObjects = controller.roomNavigation.currentRoom.InteractableObjectsInRoom;
            InteractableObject container = null;

            for (int i = 0; i < roomObjects.Count; i++)
            {
                if (containerToFill == roomObjects[i].name.ToLower())
                {
                    container = roomObjects[i];
                    break;
                }
            }

            // If full name fails, try alias lookup because players are lazy
            if (container == null)
            {
                var aliasContainer = GetShortNameList(controller, containerToFill);
                if (aliasContainer != null && roomObjects.Contains(aliasContainer))
                    container = aliasContainer;
            }

            if (container == null)
            {
                controller.LogStringWithReturn("Container " + containerToFill + " not found.");
                return;
            }

            if (container.isContainer == false)
            {
                controller.LogStringWithReturn(containerToFill + " is not a valid container.");
                return;
            }

            // Try to find the item in inventory by full name
            for (int j = 0; j < controller.playerInventory.Count; j++)
            {
                if (itemToPlace == controller.playerInventory[j].name.ToLower())
                {
                    container.contents.Add(controller.playerInventory[j]);
                    controller.playerInventory.RemoveAt(j);
                    controller.LogStringWithReturn("You put the " + itemToPlace + " in the " + container.noun);
                    return;
                }
            }

            // Try alias lookup for the item
            var aliasItem = GetShortNameList(controller, itemToPlace);
            if (aliasItem != null && controller.playerInventory.Contains(aliasItem))
            {
                container.contents.Add(aliasItem);
                controller.playerInventory.Remove(aliasItem);
                controller.LogStringWithReturn("You put the " + aliasItem.noun + " in the " + container.noun);
                return;
            }

            controller.LogStringWithReturn(itemToPlace + " not found in inventory.");
            return;
        }
    }

    // -----------------------------
    // Helper: place ALL items in a container
    // -----------------------------
    void PlaceAllInContainer(GameController controller, string[] separatedInputWords, int prepIndex)
    {
        // Build container name
        string containerName = string.Join(" ", separatedInputWords[(prepIndex + 1)..]);

        var roomObjects = controller.roomNavigation.currentRoom.InteractableObjectsInRoom;

        // Try full name first
        InteractableObject container = roomObjects.Find(o => o.name.ToLower() == containerName);

        // Try alias if needed
        if (container == null)
        {
            var alias = GetShortNameList(controller, containerName);
            if (alias != null && roomObjects.Contains(alias))
                container = alias;
        }

        if (container == null)
        {
            controller.LogStringWithReturn("Container " + containerName + " not found.");
            return;
        }

        if (!container.isContainer)
        {
            controller.LogStringWithReturn(container.noun + " is not a container. Please stop trying to store things in furniture.");
            return;
        }

        var inventory = controller.playerInventory;

        if (inventory.Count == 0)
        {
            controller.LogStringWithReturn("You have nothing to place. Your pockets are as empty as your hopes.");
            return;
        }

        // Copy items so we don't modify the list mid-loop and summon demons
        List<InteractableObject> toPlace = new List<InteractableObject>(inventory);

        foreach (var item in toPlace)
        {
            container.contents.Add(item);
            inventory.Remove(item);
        }

        controller.LogStringWithReturn("You put everything in the " + container.noun + ".");
    }
}
