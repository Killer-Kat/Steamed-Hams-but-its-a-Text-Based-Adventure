using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TextAdventure/InputActions/Inventory")]
public class Inventory : InputAction
{
    string invString;
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        invString = "You have: ";
        for (int i = 0; i < controller.playerInventory.Count; i++)
        {
            invString = invString + controller.playerInventory[i].noun + ",";
        }
        controller.LogStringWithReturn(invString);
    }
}
