using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField]
    private List<Room> masterRoomList; //List of all the rooms
    //Due to the way unity scriptable objects work they are won't be reset when we start the game again. This script fixes that.
    [SerializeField]
    private List<InteractableObject> masterContainerList; //This is not the most optimal way to handle this I am sure.
    void Awake()
    {
        for (int i = 0; i < masterRoomList.Count; i++)
        {
            masterRoomList[i].InteractableObjectsInRoom.Clear(); //clears out any old objects left in the room.
            masterRoomList[i].InteractableObjectsInRoom.AddRange(masterRoomList[i].objectItializationList); //In theory adds the content of the initial list to the active list
        }

        for (int j = 0; j < masterContainerList.Count; j++) //Remember you have to add things to the list or this does not work!
        {
            masterContainerList[j].contents.Clear();
            masterContainerList[j].contents.AddRange(masterContainerList[j].contentsItializationList);// see above
        }
    }

    
}