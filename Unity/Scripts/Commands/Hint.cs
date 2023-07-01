using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Hint")]
public class Hint : InputAction //In this house we do NOT extend monobehavior! *painting nails emoji*
    //btw if you are reading my soure code for some reason hello fellow insane person, I do not know what depths of boredom could have brought you here lol
{
    string[] hintsList = { "Try going weast.", "XYZZY", "You cant get ye flask!", "You can get a hint by using the Hint verb!", "It's an open source game, just look at the code!", "Try calling our support hotline at 1-800-555-KILLERKAT", "Control alt delete", "Ask again later", "Have you listened to my podcast The CyberKat Cafe? Check out our website cyberkatcafe.com", "That's not a bug, it's a feature!", "Have you tried steaming some hams?", "Hint: asking for a hint wont help you, try something else"};
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        int randomNumber = Random.Range(0, hintsList.Length);
        controller.LogStringWithReturn(hintsList[randomNumber]);
    }
}
