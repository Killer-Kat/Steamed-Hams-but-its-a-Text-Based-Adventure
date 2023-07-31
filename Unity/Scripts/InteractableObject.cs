using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactable Object/Item")]
public class InteractableObject : ScriptableObject
{
    public string noun = "name";
    [SerializeField]
    public bool canTake;
    [TextArea]
    public string description = "Description in room";
    [TextArea]
    public string examineDescription = "The description returned when using examine";

    public string useAction = "Default";// Default will just return an "cant use x" error so you have to set this to something else in the editor.

    [SerializeField]
    public bool isContainer;

    public List<InteractableObject> contentsItializationList; //List of objects that should be in the container at start of game, I know this bloats the class but because of how unity works we have to have this.
    [SerializeField]
    public List<InteractableObject> contents;
}
