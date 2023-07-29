using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/DialogueObject")]
public class DialogueObject : ScriptableObject //Its a data class for Dialogue
{
    //The way this works is each of these objects is a set of lines for the dialogue system, if you click a button to chose a line it will then load the next of these objects and have the next set of lines
    //Each character also has one of these objects asigned to them that will populate the dialogue system when you use TALK on them.
    //Even options that end the conversation will have one of these objects as it will award points or print npc dialogue

    //ITS SCRIPTABLE OBJECTS NOW *Said in the voice of darby boxman telling everyone to get back on the train*


    //Really got to make sure I think of every needed field here or otherwise unity will give me a bunch of _unity_self cannot be null errors when I update the script. Forcing me to remake all the scriptable objects.
    public bool IsEndOfDialogue = false;
    public string NpcDialogue;

    public int scoreToGive;
    public int weirdPoints;
    public int politePoints;

    public string HAMScommand; //For use later if I want dialogue options to trigger events with extended functionality HAMS will take care of it.

    public string option1;
    public string option2;
    public string option3;
    public string option4;

    public DialogueObject NextDialogue1;
    public DialogueObject NextDialogue2;
    public DialogueObject NextDialogue3;
    public DialogueObject NextDialogue4;

   
}
