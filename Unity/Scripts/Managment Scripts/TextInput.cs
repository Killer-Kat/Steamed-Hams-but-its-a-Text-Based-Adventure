using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;

    private GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }
    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.Keyword == separatedInputWords[0])
            {
                inputAction.RespondToInput(controller, separatedInputWords);
            }
        }

        InputComplete();
    }

    void InputComplete()
    {
        if (controller.isVerbose && controller.verboseSkip == false)
        {
            controller.DisplayRoomText(); // not sure if this will work
        }else if (controller.isVerbose)
        {
            controller.verboseSkip = false; //feels like this is a resource heavy way to do this, maybe fix later?
        }
        if(controller.isVeryVerbose == true)
        {
            controller.veryVerboseStatsText.text = "Odd Points: " + controller.oddPoints + " Polite Points: " + controller.politePoints + " Secret Number: " + controller.secretNumber;
        }
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
        controller.HAMS.Tick(); //I put this here so that it should only tick once for each command parsed.
    }
}
