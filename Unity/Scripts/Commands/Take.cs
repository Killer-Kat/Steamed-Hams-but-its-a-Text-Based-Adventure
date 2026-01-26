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
        // If the player just typed "take" and vibes, don't crash the game.
        if (separatedInputWords.Length < 2)
        {
            controller.LogStringWithReturn("Take what, exactly?");
            return;
        }

        // Normalize everything so we don't lose our minds over casing.
        NormalizeInputWords(separatedInputWords);

        // Because typing this chain 40 times is a cry for help.
        var roomObjects = controller.roomNavigation.currentRoom.InteractableObjectsInRoom;

        // Fancy grammar: "take X from Y"
        if (Array.Find(separatedInputWords, element => element == "from") == "from")
        {
            ParseTakeFromCommand(controller, separatedInputWords, roomObjects);
            return;
        }

        // Basic: "take X"
        ParseSimpleTakeCommand(controller, separatedInputWords, roomObjects);
    }

    // Turn ALL the words into lowercase so we stop calling .ToLower() like it's a personality trait.
    void NormalizeInputWords(string[] words)
    {
        for (int i = 0; i < words.Length; i++)
            words[i] = words[i].ToLower();
    }

    // Handles "take X from Y" where X might be "burnt roast" and Y might be "front door" or "oven".
    void ParseTakeFromCommand(GameController controller, string[] separatedInputWords, List<InteractableObject> roomObjects)
    {
        string itemToTake = "";
        string containerToLoot = "";

        int fromIndex = Array.IndexOf(separatedInputWords, "from");
        Debug.Log("from found at index " + fromIndex);

        // Build item name (everything between "take" and "from")
        if (fromIndex > 2)
        {
            for (int i = 1; i < fromIndex; i++)
                itemToTake += " " + separatedInputWords[i];

            itemToTake = itemToTake.Substring(1);
        }
        else
        {
            itemToTake = separatedInputWords[1];
        }

        // Build container name (everything after "from")
        for (int i = fromIndex + 1; i < separatedInputWords.Length; i++)
            containerToLoot += " " + separatedInputWords[i];

        containerToLoot = containerToLoot.Substring(1);

        // First, try to find the container by its full, proper, government-issued name.
        InteractableObject container = null;

        for (int i = 0; i < roomObjects.Count; i++)
        {
            if (containerToLoot == roomObjects[i].name.ToLower())
            {
                container = roomObjects[i];
                break;
            }
        }

        // If that fails, maybe the player used the short name because they are human.
        if (container == null)
        {
            var aliasContainer = GetShortNameList(controller, containerToLoot);
            if (aliasContainer != null && roomObjects.Contains(aliasContainer))
            {
                container = aliasContainer;
            }
        }

        // Still nothing? Then the container is imaginary.
        if (container == null)
        {
            controller.LogStringWithReturn("Container " + containerToLoot + " not found.");
            return;
        }

        // Found something, but is it actually a container or just furniture?
        if (!container.isContainer)
        {
            controller.LogStringWithReturn(container.noun + " is not a valid container.");
            return;
        }

        // Try to find the item by full name inside the container.
        for (int j = 0; j < container.contents.Count; j++)
        {
            if (itemToTake == container.contents[j].name.ToLower())
            {
                if (!container.contents[j].canTake)
                {
                    controller.LogStringWithReturn("You cant take the " + container.contents[j].name);
                    return;
                }

                controller.playerInventory.Add(container.contents[j]);
                container.contents.RemoveAt(j);

                controller.LogStringWithReturn("You take the " + itemToTake + " from the " + container.noun);
                return;
            }
        }

        // Full name failed. Time to let the alias system earn its keep.
        var aliasItem = GetShortNameList(controller, itemToTake);
        if (aliasItem != null && container.contents.Contains(aliasItem))
        {
            if (!aliasItem.canTake)
            {
                controller.LogStringWithReturn("You cant take the " + aliasItem.name);
                return;
            }

            controller.playerInventory.Add(aliasItem);
            container.contents.Remove(aliasItem);

            controller.LogStringWithReturn("You take the " + aliasItem.noun + " from the " + container.noun);
            return;
        }

        // At this point, the item is either not here or the player is hallucinating.
        controller.LogStringWithReturn(itemToTake + " not found in container " + container.noun);
    }

    // Handles "take X" where X might be "table" but the object is "dining room table".
    void ParseSimpleTakeCommand(GameController controller, string[] separatedInputWords, List<InteractableObject> roomObjects)
    {
        combinedInputWords = "";

        if (separatedInputWords.Length > 1)
        {
            for (int i = 1; i < separatedInputWords.Length; i++)
                combinedInputWords += " " + separatedInputWords[i];

            combinedInputWords = combinedInputWords.Substring(1);
        }
        else
        {
            // Technically unreachable because of the earlier guard, but hey, future-proofing.
            combinedInputWords = separatedInputWords[1];
        }

        // First, try to match the full name like a boring, literal parser.
        for (int i = 0; i < roomObjects.Count; i++)
        {
            if (combinedInputWords == roomObjects[i].name.ToLower())
            {
                if (!roomObjects[i].canTake)
                {
                    controller.LogStringWithReturn("You cant take the " + roomObjects[i].name);
                    return;
                }

                controller.playerInventory.Add(roomObjects[i]);
                roomObjects.RemoveAt(i);

                controller.LogStringWithReturn("You take the " + combinedInputWords);
                return;
            }
        }

        // Full name failed. Maybe the player typed "table" instead of "dining room table".
        var aliasObj = GetShortNameList(controller, combinedInputWords);

        // Make sure the alias actually refers to something in the room, not in inventory already.
        if (aliasObj != null && roomObjects.Contains(aliasObj))
        {
            if (!aliasObj.canTake)
            {
                controller.LogStringWithReturn("You cant take the " + aliasObj.name);
                return;
            }

            controller.playerInventory.Add(aliasObj);
            roomObjects.Remove(aliasObj);

            controller.LogStringWithReturn("You take the " + aliasObj.noun);
            return;
        }

        // If we got here, the thing they want does not exist. Tragic.
        controller.LogStringWithReturn(combinedInputWords + " not found.");
    }
}
