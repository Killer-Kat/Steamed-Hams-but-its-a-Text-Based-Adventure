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
        }
    }
}
