using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/Interactable Object/Person")] //Even though person does not inherit from Interactable object.
public class Person : ScriptableObject
    
{
    
    public string name;
    [TextArea]
    public string description;

    [SerializeField]
    public DialogueObject intialDialogue; //Stores the inital Dialogue state that the character should have at the start of the game.
    [SerializeField]
    public DialogueObject currentDialogue;
}
