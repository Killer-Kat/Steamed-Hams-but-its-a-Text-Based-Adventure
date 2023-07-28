using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour //PSYCHE its another data class! this one stores all the dialogue objects. OR IS IT!? no because C# wants to complile in a specific way and I cant be bothered to rewrite everything.
{
   public  DialogueObject testDialogue = new(false, "This is the first test", "This is also a test", testDialogue2,testDialogue3,testDialogue2,testDialogue2,"Cant b sure if this is a test", "what?");
   public static DialogueObject testDialogue2 = new(false, "This is the second test", "This is also a test", testDialogue3,testDialogue2,null,null, "sure this is a test"); //idk if being static will break this, turns out the answer is yes!
    public static DialogueObject testDialogue3 = new(false, "ABC", "123", testDialogue2, testDialogue2, testDialogue2, testDialogue2);

    //public DialogueObject GetDialogueObject()
    //{
    //    return 
    //}
}
