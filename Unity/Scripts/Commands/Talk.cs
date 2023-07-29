using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "TextAdventure/InputActions/Talk")]
public class Talk : InputAction
{
    // Start is called before the first frame update
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if(separatedInputWords.Length > 2)
        {
            //combine stuff
            Debug.Log("talk length > 1");
        }
        else
        {
            for (int i = 0; i < controller.roomNavigation.currentRoom.peopleInRoom.Count; i++)
            {
                Debug.Log(controller.roomNavigation.currentRoom.peopleInRoom[i].name);
                if(controller.roomNavigation.currentRoom.peopleInRoom[i].name.ToLower() == separatedInputWords[1])
                {
                    controller.dialogueController.StartDialogue(controller.roomNavigation.currentRoom.peopleInRoom[i].currentDialogue, controller.roomNavigation.currentRoom.peopleInRoom[i].name);
                }
            }
        }
    }
}
