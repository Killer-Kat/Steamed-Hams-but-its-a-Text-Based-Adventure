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
    private bool isContainer;
    [SerializeField]
    public List<InteractableObject> contents;
}
