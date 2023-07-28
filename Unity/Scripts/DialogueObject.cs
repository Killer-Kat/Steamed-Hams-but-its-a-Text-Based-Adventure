using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject //Its a data class for Dialogue
{
    //The way this works is each of these objects is a set of lines for the dialogue system, if you click a button to chose a line it will then load the next of these objects and have the next set of lines
    //Each character also has one of these objects asigned to them that will populate the dialogue system when you use TALK on them.
    //Even options that end the conversation will have one of these objects as it will award points or print npc dialogue

    public bool IsEndOfDialogue { get; private set; }
    public string NpcDialogue { get; private set; }

    public string option1 { get; private set; }
    public string option2 { get; private set; }
    public string option3 { get; private set; }
    public string option4 { get; private set; }

    public DialogueObject NextDialogue1 { get; private set; }
    public DialogueObject NextDialogue2 { get; private set; }
    public DialogueObject NextDialogue3 { get; private set; }
    public DialogueObject NextDialogue4 { get; private set; }

    // Private constructor to prevent external instantiation
    private DialogueObject()
    {
        // Default constructor body, if any initialization logic is needed.
    }

    private void Initialize(bool endOfDialogue, string npcDialogue, string opt1, string opt2, string opt3, string opt4, DialogueObject nd1, DialogueObject nd2, DialogueObject nd3, DialogueObject nd4)
    {
        IsEndOfDialogue = endOfDialogue;
        NpcDialogue = npcDialogue;
        option1 = opt1;
        option2 = opt2;
        option3 = opt3;
        option4 = opt4;
        NextDialogue1 = nd1;
        NextDialogue2 = nd2;
        NextDialogue3 = nd3;
        NextDialogue4 = nd4;
    }

    public static DialogueObject Create(bool endOfDialogue, string npcDialogue, string opt1, string opt2, string opt3, string opt4,DialogueObject nd1, DialogueObject nd2, DialogueObject nd3, DialogueObject nd4)
    {
        DialogueObject dialogueObject = new DialogueObject();
        dialogueObject.Initialize(endOfDialogue, npcDialogue, opt1, opt2,opt3,opt4, nd1, nd2, nd3, nd4);
        return dialogueObject;
    }
}
