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
        "You push the Enter key very dramatically.",
        "You mean to speak, to move. But nothing comes out, you are paralyzed for a breif moment.",
        "You observe a solemn moment of silence.",
        "You forget to type any words into the parser, whoops!",
        "Parser? I hardly know er!",
        "You think about doing something, but then decide not to.",
        "You pause, everything looks different, for a breif moment it almost seems like you are in some kind of retro computer game. But no that's rediculous, and you reutrn to your senses.",
        "Oh? Sorry did you say something?",
        "Hmm? Did you say something just now? Sorry I wasn't listening.",
        "Huh? Thats odd... Must be the wind.",
        "Sorry I cant hear you, you are going to have to speak up!",
        "Did you just press the Enter key by accident? Please be careful this is serious.",
        "And then I says, I says: Enter? I hardly even knew er! Oh, hello Seymour...",
        "Hey was that a blank command? You better knock it off if you don't wanna get stuck like that!"
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
                quip = "You say ERROR no quip found";

            controller.LogStringWithReturn(quip);
            InputComplete();
            controller.blankInputs += 1;
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
