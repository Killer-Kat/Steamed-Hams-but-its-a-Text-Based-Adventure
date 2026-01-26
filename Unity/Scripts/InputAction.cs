using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    public string Keyword;

    public abstract void RespondToInput(GameController controller, string[] separatedInputWords);

    public InteractableObject GetShortNameList(GameController controller, string word)
    {
        for (int i = 0; i < controller.playerInventory.Count; i++)
        {
            string fullName = controller.playerInventory[i].noun.ToLower();

            // skip single-word nouns
            if (!fullName.Contains(' '))
                continue;

            // split the multi-word noun and get last word
            string[] words = fullName.Split(' ');
            string lastWord = words[words.Length - 1];

            // check if the last word matches the input
            if (lastWord == word)
                return controller.playerInventory[i];
        }
        for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
        {
            string fullName = controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun.ToLower();

            // skip single-word nouns
            if (!fullName.Contains(' '))
                continue;

            // split the multi-word noun and get last word
            string[] words = fullName.Split(' ');
            string lastWord = words[words.Length - 1];

            // check if the last word matches the input
            if (lastWord == word)
                return controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i];
        }
        // Now search inside *all* container contents for current room because nothing is easy, maybe I will also let it search containers in your inventory
        foreach (var obj in controller.roomNavigation.currentRoom.InteractableObjectsInRoom)
        {
            if (!obj.isContainer)
                continue;

            foreach (var item in obj.contents)
            {
                string fullName = item.noun.ToLower();

                if (!fullName.Contains(' '))
                    continue;

                string[] words = fullName.Split(' ');
                string lastWord = words[words.Length - 1];

                if (lastWord == word)
                    return item;
            }
        }

        return null; //Return the slabbbbb
    }

}
