using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAMS : MonoBehaviour //H.A.M.S Hastly Asembled Management Script
{
    public GameController controller;

   public DialogueObject IntroDobj;
    public DialogueObject ChalmersEntersKitchenDobjWindow; //dobj to use for CEK scene if window is open
    public DialogueObject ChalmersEntersKitchenDobjNoWindow;

    public bool isKitchenOnfire = false;
    public bool isHouseOnFire = false;
    public int ovenKitchenFireCountdown; //may need to adjust this //Its the fire countdown do do dee do, do de de do 
    public int kitchenFireSpreadCountdown; 
    public int burningHouseDeathCountdown; //tracks the amount of cupcakes you baked for the sugar princess, just kidding does what you think it does.

    public InteractableObject tv;
    public bool isTVon = false;
    public int tvSecretCounter = 0;
    bool isTvFixed = false;

    public InteractableObject table;
    public bool isGrossFoodOnTable = false;

    public Person chalmers;
    public Person jermey;

    public InteractableObject combomeal;
    public InteractableObject ribwich;
    public DialogueObject krustyburgerbreak;

    public Room Kitchen;
    public Exit KrustyBurgerExit;
    public DialogueObject KitchenFireDobj;
    public InteractableObject window;
    public bool isWindowOpen = false;
    public InteractableObject oven;
    public bool isOvenOn = true;

    public DialogueObject HouseFireDobj;

    public DialogueObject LunchRoastDobj;

    public bool isSteamedHams = false;
    public bool didChalmersEat = false;

    public InteractableObject steamedHams;
    // Start is called before the first frame update

    public void IntroScene()
    {
        controller.dialogueController.StartDialogue(IntroDobj, "Chalmers");
    }
    public void Tick()//used for the countdowns.
    {
        if (oven.contents.Count != 0 && isOvenOn == true)
        {
            ovenKitchenFireCountdown -= 1;
        }
        if(ovenKitchenFireCountdown == 0)
        {
            KitchenOnFire();
        }
        if(isKitchenOnfire == true)
        {
            kitchenFireSpreadCountdown -= 1;
        }
        if(kitchenFireSpreadCountdown == 0)
        {
            HouseFire();
        }
        if(isHouseOnFire == true)
        {
            burningHouseDeathCountdown -= 1;
        }
        if(burningHouseDeathCountdown == 0)
        {
            BurningEnding();
        }

    }
    public void KitchenOnFire()
    {
        isKitchenOnfire = true;
        if(controller.roomNavigation.currentRoom.rooomName == "Kitchen" || controller.roomNavigation.currentRoom.rooomName == "Dining Room")
        {
            controller.dialogueController.StartDialogue(KitchenFireDobj, "Chalmers");
        }
        Kitchen.description = "A small square teal colored kitchen, its somewhat hard to make out any other details due to the fact that it is currently on fire!";
    }

    public void ChalmersEntersKitchen()
    {
        if (controller.roomNavigation.currentRoom.rooomName == "Kitchen" && isWindowOpen == false)
        {
            controller.dialogueController.StartDialogue(ChalmersEntersKitchenDobjNoWindow, "Chalmers");
        }
        else if (controller.roomNavigation.currentRoom.rooomName == "Kitchen" && isWindowOpen == true)
        {
            controller.dialogueController.StartDialogue(ChalmersEntersKitchenDobjWindow, "Chalmers");
        }
    }
    public void chalmersGoodbye()
    {
        if (controller.politePoints <= 6 && controller.oddPoints > 6)
        {
            controller.LogStringWithReturn("Chalmers: Well Seymore I must say you are an odd fellow.");
            controller.updateScore(1);
        }
        else if (controller.politePoints > 6 && controller.oddPoints > 6)
        {
            controller.LogStringWithReturn("Chalmers: Well Seymore I must say you are an odd yet polite fellow.");
            controller.updateScore(3);
        }
        else if (controller.politePoints <= 0 && controller.oddPoints > 6)
        {
            controller.LogStringWithReturn("Chalmers: Well Seymore I must say you are a rude and odd man.");
            controller.updateScore(-5);
        }
        else if (controller.politePoints > 6 && controller.oddPoints < 6)
        {
            controller.LogStringWithReturn("Chalmers: Well Seymore I must say you are a polite fellow.");
            controller.updateScore(4);
        }
        else if (controller.politePoints <= 0 && controller.oddPoints < 6)
        {
            controller.LogStringWithReturn("Chalmers: Well Seymore I must say you are a rude jerk!");
            controller.updateScore(-4);
        }
        else
        {
            controller.LogStringWithReturn("Chalmers: Well Seymore I must say you are a boring drag.");
        }

        if (isSteamedHams == true)
        {
            controller.LogStringWithReturn("Chalmers: But you steam a good ham.");
        }
        else if (didChalmersEat == false)
        {
            controller.LogStringWithReturn("Chalmers: But I'm disappointed I didn't get to eat anything.");
        }
        else if (controller.politePoints <= -1)
        {
            controller.LogStringWithReturn("Chalmers: But I'm happy to say YOU'RE FIRED!");
        }

        controller.ShowEndGamePopup(controller.score,controller.oddPoints,controller.politePoints);
    }

    public void LunchMealLogic()
    {
        for (int i = 0; i < table.contents.Count; i++)
        {
            if(table.contents[i].isGrossFood == true)
            {
                isGrossFoodOnTable = true;
            }
            if (table.contents[i].noun == "steamed hams")
            {
                controller.updateScore(5);
                didChalmersEat = true;
            } else if (table.contents[i].noun == "combo meal")
            {
                controller.updateScore(2);
                controller.UpdateOddPoints(2);
                controller.UpdatePolitePoints(-2);
                didChalmersEat = true;
            } else if (table.contents[i].noun == "burnt roast")
            {
                controller.updateScore(-1);
                controller.UpdateOddPoints(5);
                controller.UpdatePolitePoints(-5);
                    didChalmersEat = true;
                controller.dialogueController.UnpackFromDialogueObject(LunchRoastDobj); //I am going to make a note here that I changed order that the dialogue controller unpacks dialogue objects so it runs the HAMS commands last so that I could get this to work right. Honestly it was driving me crazy, but thankfully I had my programmer socks on and was able to realize that I made the entire thing so I could just change it to work how I wanted. They really do make you better at coding! :3

            }
        }
    }
    public void HouseFire()
    {
        isHouseOnFire = true;
        controller.dialogueController.StartDialogue(HouseFireDobj, "Mother");
    }
    public void BurningEnding()
    {
        controller.LogStringWithReturn("Your house has burnt down killing everyone inside.");
        controller.LogStringWithReturn("You are dead.");
        controller.displayText.color = Color.red;
        controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints);
    }
    public void TakeInputFromDialogue(string command)//Take command strings from dialogue objects and use them to trigger events elsewhere in the code.
    {
        switch (command)
        {
            default:
                Debug.LogError("Invalid HAMS command entered");
                break;
            case "nintendo":
                chalmers.description = "Your boss, the Super Nintendo is here you had better be sure to impress him after your clearly just forget his name...";
                //chalmers.name = "Super Nintendo Chalmers"; //CUrrently Breaks the talk command and I cannot be bothered to fix it atm
 /** Everything you say to me puts me one step closer to the edge and I'm about to **/break; //Sorry had to, it gets stuck in my head everytime I use switch statements.
            case "givecombomeal":
                controller.playerInventory.Add(combomeal);
                jermey.currentDialogue = krustyburgerbreak;
                break;
            case "ribwich":
                controller.playerInventory.Add(ribwich);
                jermey.currentDialogue = krustyburgerbreak;
                break;
            case "bigsmoke":
                controller.playerInventory.Add(combomeal);
                controller.playerInventory.Add(ribwich);
                jermey.currentDialogue = krustyburgerbreak;
                break;
            case "endgame":
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints);
                break;
            case "steamedhams":
                isSteamedHams = true;
                break;
            case "startlunch":
                LunchMealLogic();
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
                    for (int i = 0; i < controller.playerInventory.Count; i++)
                    {
                        if(controller.playerInventory[i].noun == "combo meal")
                        {
                            controller.playerInventory.RemoveAt(i);//should remove the combo meal from the players inventory.
                            controller.playerInventory.Add(steamedHams);
                            controller.LogStringWithReturn("You put the meal on a serving platter.");
                            return;
                        }
                    }
                    for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++) //Is the player DROPING the combo meal on the kitchen floor and then trying to use it an edge case? yes. Is it the first thing my hacker brain thought of when I made the previous loop? Also yes.
                    {
                        if(controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun == "combo meal")
                        {
                            controller.roomNavigation.currentRoom.InteractableObjectsInRoom.RemoveAt(i);//should remove the combo meal from the rooms inventory.
                            controller.playerInventory.Add(steamedHams);
                            controller.LogStringWithReturn("You put the meal on a serving platter. You hope your boss wont notice you dropped it on the floor first.");
                            return;
                        }
                    }
                    
                }
                else
                {
                    controller.LogStringWithReturn("If you were in the kitchen you could put this on a nice serving platter.");
                }
                break;
            case "canned laughter":
                controller.LogStringWithReturn("You open the can and for a split second feel the presence of a live studio audience as the sound of laughter escapes the steel container.");
                for (int i = 0; i < controller.playerInventory.Count; i++)
                {
                    if(controller.playerInventory[i].noun == "canned laughter")
                    {
                        controller.playerInventory.RemoveAt(i);
                    }
                }
                controller.updateScore(2);
                break;
            case "bucket":
                if(controller.roomNavigation.currentRoom.rooomName == "Kitchen")
                {
                    if(isKitchenOnfire == true)
                    {
                        for (int i = 0; i < controller.playerInventory.Count; i++)
                        {
                            if (controller.playerInventory[i].noun == "bucket")
                            {
                                controller.playerInventory[i].contents.Clear();
                            }
                        }
                        isKitchenOnfire = false;
                        controller.updateScore(4);
                        controller.LogStringWithReturn("You dump the bucket out and extinguish the fire!");
                        if(isHouseOnFire == true)
                        {
                            controller.updateScore(-2);
                            controller.LogStringWithReturn("It's too bad the rest of your house is on fire, because it relights the kitchen fire! If only you had done something sooner!");
                            isKitchenOnfire = true;
                        }
                        else { Kitchen.description = "A small burnt formerly teal colored kitchen, almost everything in the room is scorched or has burnt away."; }
                    }
                    else { controller.LogStringWithReturn("The bucket is already full of ice, so you arent sure what you would use it for at the moment."); }
                } else { controller.LogStringWithReturn("Try as you might you cant seem to think of a use for a bucket full of ice right now."); }
                break;
            case "phone": // In the final version I want to have a UI phone where you can dial numbers, maybe even some secret numbers. But thats a lot of work and not in scope right now
                controller.LogStringWithReturn("Cyberkat Cafe, Killer Kat speaking. Oh you are stuck in a text based adventure game? Have you tried using the Hint verb?, maybe it will help you. *click*");
                break;
            case "tv":
                isTVon = !isTVon;
                

                if(isTVon == true && isTvFixed == false)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, it's missing an antenna. It's currently just showing static yet something about it seems rather odd...";
                    controller.LogStringWithReturn("The TV crackles to life, but its just showing static.");
                } else if (isTVon == false && isTvFixed == false)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, it's missing an antenna. It's currently off yet something about it seems rather odd...";
                    controller.LogStringWithReturn("The TV shuts off with a flash, leaving nothing but a black screen.");
                }else if (isTVon == true && isTvFixed == true)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, you replaced it's antenna. It's currently just showing static yet something about it seems rather odd it almost resembles some kind of head.";
                    controller.LogStringWithReturn("The TV crackles to life, but its just showing static despite the antenna.");
                    
                }
                else if(isTVon == false && isTvFixed == true)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, you replaced it's antenna. It's currently off yet something about it seems rather odd it almost resembles some kind of head.";
                    controller.LogStringWithReturn("The TV shuts off with a flash, leaving nothing but a black screen.");
                    tvSecretCounter += 1;
                }
                if(tvSecretCounter == 5)
                {
                    controller.LogStringWithReturn("Suddely the TV starts playing a strange video.");
                    controller.LogStringWithReturn("Odd voice: Sometimes you're just steaming some hams when you stumble across something in the most unexpected of places, so today we're looking at the 5 most unexplained secrets in the Steamed Hams Text Based Adventure.");
                    controller.LogStringWithReturn("Odd voice: Number 1. The Backrooms some players reported that when moving between rooms of the house sometimes they were randomly teleported to the backrooms, a mysterious creepy pasta thats been making the rounds online.");
                    controller.LogStringWithReturn("Odd voice: Thanks to my friend the hacker EL BARTO for digging through the game files and finding that the player can be teleported here yourself by simply going weast.");
                    controller.LogStringWithReturn("Odd voice: Number 2. The Odd Hea *BZZT* Suddenly the TV goes back to static");
                }
                break;
            case "hanger":
                if(controller.roomNavigation.currentRoom.rooomName == "Living Room")
                {
                    isTvFixed = true;
                    for (int i = 0; i < controller.playerInventory.Count; i++)
                    {
                        if (controller.playerInventory[i].noun == "hanger")
                        {
                            controller.playerInventory.RemoveAt(i);
                            controller.LogStringWithReturn("You use the coat hanger to make a new TV antenna.");
                            return;
                        }
                    }
                    for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++) //yeah this one too
                    {
                        if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun == "hanger")
                        {
                            controller.roomNavigation.currentRoom.InteractableObjectsInRoom.RemoveAt(i);
                            controller.LogStringWithReturn("You use the coat hanger to make a new TV antenna.");
                            return;
                        }
                    }
                }
                else { controller.LogStringWithReturn("You dont use the hanger, an odd thought pops into your head that you might need it for something else."); }
                break;
            case "oven":
                if(isOvenOn == true) { controller.LogStringWithReturn("You turned the oven off."); oven.examineDescription = "A cheap white oven with a 4 burner stove and a broken timer. It is currently off."; } else { controller.LogStringWithReturn("You turned the oven on."); oven.examineDescription = "A cheap white oven with a 4 burner stove and a broken timer. It is currently on.";}
                isOvenOn = !isOvenOn;
                break;
            case "window":
                if (isWindowOpen == true) { controller.LogStringWithReturn("You close the window with a satisfying thud!"); window.examineDescription = "A large closed window that overlooks the Krustyburger, if you were into fitness this would be a good place to strech your calves."; controller.ResetRoomExits(Kitchen); controller.roomNavigation.UnpackExitsInRoom(); } else { controller.LogStringWithReturn("Its a bit heavy but you manage to open the window. The smell of fast food grease wafts in on the breeeze."); window.examineDescription = "A large open window, fast food grease permeates the air in your kitchen."; Kitchen.exits.Add(KrustyBurgerExit); }
                isWindowOpen = !isWindowOpen;
                break; //Please refrain from breaking the window
        }
    }

}
