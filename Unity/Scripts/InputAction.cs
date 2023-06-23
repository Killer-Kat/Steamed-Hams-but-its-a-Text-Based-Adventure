using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    public string Keyword;

    public abstract void RespondToInput(GameController controller, string[] separatedInputWords);

}
