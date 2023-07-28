using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject //Its a data class for Dialogue
{
    //The way this works is each of these objects is a set of lines for the dialogue system, if you click a button to chose a line it will then load the next of these objects and have the next set of lines
    //Each character also has one of these objects asigned to them that will populate the dialogue system when you use TALK on them.
    //Even options that end the conversation will have one of these objects as it will award points or print npc dialogue

    public bool IsEndOfDialogue = false; //Is this Dialougue object the last one in the tree? 
    public string npcDialogue; //Whatever the npc says before the dialogue options print
    
    public string option1;
    public string option2;
    public string option3 = "NA";
    public string option4 = "NA"; //NA is used in the Dialogue system to indicate an unused option.

    public DialogueObject NextDialogue1;
    public DialogueObject NextDialogue2;
    public DialogueObject NextDialogue3;
    public DialogueObject NextDialogue4;

    public DialogueObject(bool endOfDialogue, string opt1, string opt2, DialogueObject nd1, DialogueObject nd2, DialogueObject nd3, DialogueObject nd4, string opt3 = "NA", string opt4 = "NA")
    {
        IsEndOfDialogue = endOfDialogue;

        option1 = opt1;
        option2 = opt2;
        option3 = opt3;
        option4 = opt4;

        NextDialogue1 = nd1;
        NextDialogue2 = nd2;
        NextDialogue3 = nd3;
        NextDialogue4 = nd4;
    }
}
