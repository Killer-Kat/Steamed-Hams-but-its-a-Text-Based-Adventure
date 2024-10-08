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
    [SerializeField]
    public bool showContainerContents; //show container contents in room when verbose mode is off (for example things on a table you should be able to just see)
    [SerializeField]
    public bool isGrossFood; //Is the object a gross food that will trigger extra lines when added to table at lunch or if eaten (when eat command is added)
    public bool canBeOpened;

    public List<InteractableObject> contentsItializationList; //List of objects that should be in the container at start of game, I know this bloats the class but because of how unity works we have to have this.
    [SerializeField]
    public List<InteractableObject> contents;
}
