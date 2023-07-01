using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Interactable Object")]
public class InteractableObject : ScriptableObject
{
    public string noun = "name";
    [SerializeField]
    private bool canTake;
    [TextArea]
    public string description = "Description in room";
    [TextArea]
    public string examineDescription = "The description returned when using examine";

    [SerializeField]
    public bool isContainer;

    public List<InteractableObject> contentsItializationList; //List of objects that should be in the container at start of game, I know this bloats the class but because of how unity works we have to have this.
    [SerializeField]
    public List<InteractableObject> contents;
}
