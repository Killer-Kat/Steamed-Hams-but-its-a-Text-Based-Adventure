using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/Container")]
public class Container : InteractableObject
{
    [SerializeField]
    List<InteractableObject> contents;
}
