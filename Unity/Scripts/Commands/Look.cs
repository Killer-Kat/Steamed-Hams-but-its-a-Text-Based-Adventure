using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Look")]//When I have more time I'll make multiple copies of these objects with different keywords for synonyms
public class Look : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        string[] fillerWords = { "at", "the", "to", "on", "in", "my", "for", "Cromulent", "Embiggen" }; //Words we want to not try to include in multi word nouns
        if (separatedInputWords.Length <= 1) //Just looking shows the room.
        {
            controller.DisplayRoomText();
        }

        else if (separatedInputWords.Length == 2)// two words, one of them is look so the other one must be the item.
        {
            //Debug.Log("Looking for: " + separatedInputWords[1]);
            for (int i = 0; i < controller.playerInventory.Count; i++)
            {
                //Debug.Log("Look command finds: " + controller.playerInventory[i].noun); 
                if (separatedInputWords[1].ToLower() == controller.playerInventory[i].noun.ToLower())//noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
                {
                    LogItem(controller.playerInventory[i]);
                    return;
                }
            }
            for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)
            {
                if (separatedInputWords[1].ToLower() == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun.ToLower())//noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
                {
                    LogItem(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i]);
                    return;
                }
            }
            GetShortNameList(controller, separatedInputWords[1].ToLower());
            controller.LogStringWithReturn("Could not find: " + separatedInputWords[1]);

        }

        else //Item to look at is multiple words long

        {
            string itemToFind = "";

            for (int i = 1; i < separatedInputWords.Length; i++)
            {
                string w = separatedInputWords[i].ToLower();

                // Only ~~good Boys~~ words that are not in the filter list get added.
                if (System.Array.IndexOf(fillerWords, w) == -1)
                {
                    itemToFind = itemToFind + " " + w;
                }
            }

            // Remove the first space if we added it .-.
            if (itemToFind.Length > 0)
            {
                itemToFind = itemToFind.Substring(1);
            }

            //Debug.Log("Look searching for: " + itemToFind);
            for (int i = 0; i < controller.playerInventory.Count; i++) //look in the player inv first
            {
                //Debug.Log("Look command finds: " + controller.playerInventory[i].noun); 
                if (itemToFind.ToLower() == controller.playerInventory[i].noun.ToLower())
                { 
                    LogItem(controller.playerInventory[i]);
                    
                }
            }
            for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++)//Then check the room
            {
                if (itemToFind.ToLower() == controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun.ToLower())//noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
                {
                    LogItem(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i]);
                    return;
                }
            }
        }
        void LogItem(InteractableObject item) //this is so we can skip a lot of repeated code, still need to implement
        {
            controller.LogStringWithReturn(item.examineDescription);

            if (item.isContainer)
            {
                string contentsText = "It contains :";
                for (int j = 0; j < item.contents.Count; j++)
                {
                    contentsText += " " + item.contents[j].noun.ToLower();
                }
                controller.LogStringWithReturn(contentsText);
            }
        }
    }
}
