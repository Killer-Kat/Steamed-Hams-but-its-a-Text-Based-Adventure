using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Look")]//When I have more time I'll make multiple copies of these objects with different keywords for synonyms
public class Look : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length <= 1)
        {
            controller.DisplayRoomText();
        } else if (separatedInputWords.Length == 2)
        {
            //Debug.Log("Looking for: " + separatedInputWords[1]);
            for (int i = 0; i < controller.playerInventory.Count; i++)
            {
                //Debug.Log("Look command finds: " + controller.playerInventory[i].noun); 
                if(separatedInputWords[1].ToLower() == controller.playerInventory[i].noun.ToLower())//noun is a lowercase name used via the parser. .name will give its unity engine name which we do not want
                {
                    controller.LogStringWithReturn(controller.playerInventory[i].examineDescription);
                }
            } 
            
        }else
        {
            string itemToFind = "";
            for (int i = 1; i < separatedInputWords.Length; i++)
            {
                itemToFind = itemToFind + " " + separatedInputWords[i];
            }
            itemToFind = itemToFind.Substring(1);
            //Debug.Log("Look searching for: " + itemToFind);
            for (int i = 0; i < controller.playerInventory.Count; i++)
            {
                //Debug.Log("Look command finds: " + controller.playerInventory[i].noun); 
                if (itemToFind.ToLower() == controller.playerInventory[i].noun.ToLower())//remember to keep everything lowercase!
                    controller.LogStringWithReturn(controller.playerInventory[i].examineDescription);
                }
            }
        }
    }
}
