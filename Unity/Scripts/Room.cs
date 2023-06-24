using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/Room")]
public class Room : ScriptableObject
{
    [TextArea]
    public string description;
    public string rooomName;
    public Exit[] exits;
    public List<InteractableObject> objectItializationList; //List of objects that should be in the room at start of game.

    public List<InteractableObject> InteractableObjectsInRoom;

    public List<Person> peopleItializationList; //Oh my god you have a list! 

    public List<Person> peopleInRoom;
}
