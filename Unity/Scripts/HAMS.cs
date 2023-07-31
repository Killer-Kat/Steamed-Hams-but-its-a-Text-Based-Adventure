using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAMS : MonoBehaviour //H.A.M.S Hastly Asembled Management Script
{
    public GameController controller;

   public DialogueObject IntroDobj;

    public bool isKitchenOnfire = false;
    public bool isHouseOnFire = false;
    public int ovenKitchenFireCountdown; //Its the fire countdown do do dee do, do de de do 

    public bool isSteamedHams = false;

    public InteractableObject steamedHams;
    // Start is called before the first frame update

    public void IntroScene()
    {
        controller.dialogueController.StartDialogue(IntroDobj, "Chalmers");
    }
    
    public void TakeInputFromDialogue(string command)//Take command strings from dialogue objects and use them to trigger events elsewhere in the code.
    {
        switch (command)
        {
            default:
                Debug.LogError("Invalid HAMS command entered");
                break;
        }

    }
    public void UseActionTree(string key) //This is a list containing all the logic that gets trigger when you use items
    {
        switch (key)
        {
            default:
                controller.LogStringWithReturn("You cant use this");//later could update this to return the item
                break;
            case "combomeal":
                if (controller.roomNavigation.currentRoom.rooomName == "Kitchen")
                {
                    controller.playerInventory.Add(steamedHams);
                    controller.LogStringWithReturn("You put the meal on a serving platter.");
                }
                else
                {
                    controller.LogStringWithReturn("If you were in the kitchen you could put this on a nice serving platter.");
                }
                break;
        }
    }

}
