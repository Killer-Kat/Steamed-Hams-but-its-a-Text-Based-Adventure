using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour //PSYCHE its another data class! this one stores all the dialogue objects. OR IS IT!? no because C# wants to complile in a specific way and I cant be bothered to rewrite everything.
{
    public DialogueObject testDialogue;
    public DialogueObject testDialogue2;
    public DialogueObject testDialogue3;

    private void Start()
    {
        InitializeDialogueTree();
    }
    // Call this method in Awake, Start, or any other appropriate initialization method
    private void InitializeDialogueTree() //For some F**King reason the complier is asigning null values to these object varibles instead of the existing varibles that hold the objects that I F**king put at the top of the script so you have to initalize the whole F**king thing backwards otherwise its full of null values and breaks everything.
    {
       /** Debug.Log("Dialogue Tree Initialization started");
        // Initialize the DialogueObject instances using the factory method
        testDialogue3 = DialogueObject.Create(
            endOfDialogue: false,
            npcDialogue: "This is the third and final test.",
            opt1: "123",
            opt2: "sure this is a test",
            opt3: "Tester? I barely know her!",
            opt4: "click me I dare you",
            nd1: null,
            nd2: null,
            nd3: null,
            nd4: null
        );

        testDialogue2 = DialogueObject.Create(
            endOfDialogue: false,
            npcDialogue: "This is the second test",
            opt1: "This is also a test",
            opt2: "sure this is a test",
            opt3: null,
            opt4: null,
            nd1: testDialogue3,
            nd2: testDialogue,
            nd3: null,
            nd4: null
        ); ;
        testDialogue = DialogueObject.Create(
            endOfDialogue: false,
            npcDialogue: "This is the first test",
            opt1: "This is also a test",
            opt2: "Cant b sure if this is a test",
            opt3:"opt 3",
            opt4: "C# why u gotta do me dirty dawg?",
            nd1: testDialogue2,
            nd2: testDialogue3,
            nd3: testDialogue2,
            nd4: testDialogue2
        );
        



        //Debug.Log(testDialogue.NextDialogue1);
       // Debug.Log(testDialogue3.NextDialogue1); **/
    }
}
