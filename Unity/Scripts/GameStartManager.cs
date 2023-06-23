using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField]
    private List<Room> masterRoomList; //List of all the rooms
    //Due to the way unity scriptable objects work they are won't be reset when we start the game again. This script fixes that.
    void Awake()
    {
        for (int i = 0; i < masterRoomList.Count; i++)
        {
            masterRoomList[i].InteractableObjectsInRoom.Clear(); //clears out any old objects left in the room.
            masterRoomList[i].InteractableObjectsInRoom.AddRange(masterRoomList[i].objectItializationList); //In theory adds the content of the initial list to the active list
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
