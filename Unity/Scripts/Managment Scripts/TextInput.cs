using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;

    private GameController controller;

    [SerializeField]
    private List<string> blankQuips = new List<string>()
    {
        "You stare into the void. It stares back.",
        "Silence... Is that your next move?",
        "You say nothing. The room waits.",
        "No words came out. Very dramatic.",
        "Placeholder? I hardly know er!"
    };

    private void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }
    void AcceptStringInput(string userInput)
    {
        // If the input is empty or only whitespace, reply with a quip
        if (string.IsNullOrWhiteSpace(userInput))
        {
            string quip;
            if (blankQuips != null && blankQuips.Count > 0)
                quip = blankQuips[Random.Range(0, blankQuips.Count)];
            else
                quip = "You say nothing.";

            controller.LogStringWithReturn(quip);
            InputComplete();
            return;
        }

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
