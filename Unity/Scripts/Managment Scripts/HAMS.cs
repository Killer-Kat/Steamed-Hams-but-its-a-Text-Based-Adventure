using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAMS : MonoBehaviour //H.A.M.S Hastly Asembled Management Script
{
    public GameController controller;

    public DialogueObject IntroDobj;
    public DialogueObject ChalmersEntersKitchenDobjWindow; //dobj to use for CEK scene if window is open
    public DialogueObject ChalmersEntersKitchenDobjNoWindow;
    public int chalmersEnterKitchenCountdown; //Counter for having chalmers enter the kitchen before lunch
    public bool skipChalmersEntersKitchen = false;

    public bool isKitchenOnfire = false;
    public bool isHouseOnFire = false;
    public int ovenKitchenFireCountdown; //may need to adjust this //Its the fire countdown do do dee do, do de de do 
    public int kitchenFireSpreadCountdown;
    public int burningHouseDeathCountdown; //tracks the amount of cupcakes you baked for the sugar princess, just kidding does what you think it does.
    public bool triggerPostLunchFire = false; //In order to not break things, we are going to not trigger the first fire scene while the lunch scene happpens, if this bool is true we want to trigger that scene directly after the lunch scene.

    public InteractableObject tv;
    public bool isTVon = false;
    public int tvSecretCounter = 0;
    bool isTvFixed = false;

    public InteractableObject table;
    public bool isGrossFoodOnTable = false; //If true there should be alt lines for if chalmers did or did not eat anything durring lunch

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
    public DialogueObject LunchComboMealDobj;
    public DialogueObject LunchSteamedHamsDobj;
    public DialogueObject LunchHamburgersDobj;
    public DialogueObject LunchPerfectRoastDobj;
    public DialogueObject LunchSteamedClamsDobj;
    public DialogueObject LunchRibwichDobj;
    public DialogueObject LunchHerringDobj;
    public DialogueObject LunchAppleDobj;
    public DialogueObject LunchWineGlassDobj;
    public DialogueObject LunchBucketDobj;



    public bool isSteamedHams = false; //Has the player told chalmers that they were having "steamed clams" for dinner
    public bool didChalmersEat = false; //Used to change the final dialogue based on player action.
    public bool wineGlassesUsed = false; //Used during the lunch meal logic so that we can take the bucket off the table without scolding the player for not having wine glasses.

    public InteractableObject steamedHams;
    public InteractableObject hamburgers; //Blasphemy!
    public InteractableObject IceBucket;
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
        if (ovenKitchenFireCountdown == 0)
        {
            KitchenOnFire();
        }
        if (isKitchenOnfire == true)
        {
            kitchenFireSpreadCountdown -= 1;
        }
        if (kitchenFireSpreadCountdown == 0)
        {
            HouseFire();
        }
        if (isHouseOnFire == true)
        {
            burningHouseDeathCountdown -= 1;
        }
        if (burningHouseDeathCountdown == 0)
        {
            EndingManager("burningDeath");
        }
        if (skipChalmersEntersKitchen == false)
        {
            chalmersEnterKitchenCountdown -= 1;
            if (chalmersEnterKitchenCountdown == 0)
            {
                ChalmersEntersKitchen();
                skipChalmersEntersKitchen = true;
            }
        }

    }
    public void KitchenOnFire()
    {
        isKitchenOnfire = true;
        if (controller.roomNavigation.currentRoom.rooomName == "Kitchen" || controller.roomNavigation.currentRoom.rooomName == "Dining Room")
        {
            if (triggerPostLunchFire == false)
            {
                controller.dialogueController.StartDialogue(KitchenFireDobj, "Chalmers");
            }
            else
            {
                controller.dialogueController.UnpackFromDialogueObject(KitchenFireDobj); //Need to use unpack rather than start because in this case we are already in the dialogue system.
            }
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

        //controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints);
    }

    public void LunchMealLogic()
    {
        Debug.Log("Lunch meal Logic function triggered");
        if (isOvenOn == true && isKitchenOnfire == false && oven.contents.Count != 0 && table.contents.Count == 0)
        {
            triggerPostLunchFire = true;
            isOvenOn = false;
        }
        for (int i = 0; i < table.contents.Count; i++)
        {
            if (table.contents[i].isGrossFood == true)
            {
                isGrossFoodOnTable = true;
            }
            if (table.contents[i].noun == "wine glasses")
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchWineGlassDobj);
                table.contents.RemoveAt(i);
                wineGlassesUsed = true;
                return;
            }
            else if (table.contents[i].noun == "bucket" && wineGlassesUsed == false && table.contents.Count == 1) //Issue, the bucket is always at the first index of the table contents so it always triggers the no wine glasses condition even when the wine glasses are present as it finds the bucket first. Bandaid fix add a check that there is only 1 item on the table, obviously this will break if the player puts a non food item on the table since it won't be removed by the script. But I can fix that edge case later
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchBucketDobj);
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(IceBucket);
                table.contents.RemoveAt(i);
                return;
            }
            else if (table.contents[i].noun == "bucket" && wineGlassesUsed == true)  //still need to move the bucket off the table so the fire triggers, but dont want chalmers to scold the player for not having wine glasses.
            {
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(IceBucket);
                table.contents.RemoveAt(i);
                LunchMealLogic();//Since this does not trigger any dialogue which would run the LunchMealLogic() method again we have to call it here.
                return;
            }
            else if (table.contents[i].noun == "steamed hams")
            {
                controller.updateScore(5);
                controller.dialogueController.UnpackFromDialogueObject(LunchSteamedHamsDobj);
                didChalmersEat = true;
                table.contents.RemoveAt(i);
                return;
            }
            else if (table.contents[i].noun == "hamburgers")
            {
                controller.updateScore(5);
                controller.dialogueController.UnpackFromDialogueObject(LunchHamburgersDobj);
                didChalmersEat = true;
                table.contents.RemoveAt(i);
                return;
            }
            else if (table.contents[i].noun == "combo meal")
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchComboMealDobj);
                didChalmersEat = true;
                table.contents.RemoveAt(i);
                return;
            }
            else if (table.contents[i].noun == "ribwich")
            {
                controller.updateScore(1);
                controller.dialogueController.UnpackFromDialogueObject(LunchRibwichDobj);
                table.contents.RemoveAt(i);
                return;
                //Not going to have this count as chalmers eating, since you have the option to eat it yourself and I don't want to program that edge case right now.
            }
            else if (table.contents[i].noun == "burnt roast")
            {
                controller.updateScore(-1);
                controller.UpdateOddPoints(5);
                controller.UpdatePolitePoints(-5);
                didChalmersEat = true;
                controller.dialogueController.UnpackFromDialogueObject(LunchRoastDobj); //I am going to make a note here that I changed order that the dialogue controller unpacks dialogue objects so it runs the HAMS commands last so that I could get this to work right. Honestly it was driving me crazy, but thankfully I had my programmer socks on and was able to realize that I made the entire thing so I could just change it to work how I wanted. They really do make you better at coding! :3
                table.contents.RemoveAt(i);
                return;
            }
            else if (table.contents[i].noun == "pickled herring")
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchHerringDobj);
                table.contents.RemoveAt(i);
                return;
            }
            else if (table.contents[i].noun == "apple")
            {
                controller.updateScore(1);
                controller.dialogueController.UnpackFromDialogueObject(LunchAppleDobj);
                didChalmersEat = true;
                table.contents.RemoveAt(i);
                return;
            }

        }
        if (triggerPostLunchFire == true)//this one should be last as if true it will put us in the post lunch fire scene.
        {
            //KitchenOnFire();
            controller.dialogueController.UnpackFromDialogueObject(KitchenFireDobj); //hard coding this instead of calling the method becuase it breaks otherwise. ¯\_(.-.)_/¯ 
            isKitchenOnfire = true;
            Kitchen.description = "A small square teal colored kitchen, its somewhat hard to make out any other details due to the fact that it is currently on fire!";
            Debug.Log("post lunch fire triggered");
        }
        //Note to self, add a catch here that moves us onto the post lunch scene 
    }
    public void HouseFire()
    {
        isHouseOnFire = true;
        controller.dialogueController.StartDialogue(HouseFireDobj, "Mother");
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
                /** Everything you say to me puts me one step closer to the edge and I'm about to **/
                break; //Sorry had to, it gets stuck in my head everytime I use switch statements.
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
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints,"error");
                break;
            case "steamedhams":
                isSteamedHams = true;
                break;
            case "startlunch":
                LunchMealLogic();
                break;
            case "speedrun":
                EndingManager("speedrun");
                break;
            case "fired":
                EndingManager("fired");
                break;
        }

    }
    public void UseActionTree(string key) //This is a list containing all the logic that gets triggered when you use items
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
                        if (controller.playerInventory[i].noun == "combo meal")
                        {
                            controller.playerInventory.RemoveAt(i);//should remove the combo meal from the players inventory.
                            if (isSteamedHams == true)
                            {
                                controller.playerInventory.Add(steamedHams);
                            }
                            else
                            {
                                controller.playerInventory.Add(hamburgers);
                            }
                            controller.LogStringWithReturn("You put the meal on a serving platter.");
                            return;
                        }
                    }
                    for (int i = 0; i < controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Count; i++) //Is the player DROPING the combo meal on the kitchen floor and then trying to use it an edge case? yes. Is it the first thing my hacker brain thought of when I made the previous loop? Also yes.
                    {
                        if (controller.roomNavigation.currentRoom.InteractableObjectsInRoom[i].noun == "combo meal")
                        {
                            controller.roomNavigation.currentRoom.InteractableObjectsInRoom.RemoveAt(i);//should remove the combo meal from the rooms inventory.
                            if (isSteamedHams == true)
                            {
                                controller.playerInventory.Add(steamedHams);
                            }
                            else
                            {
                                controller.playerInventory.Add(hamburgers);
                            }
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
                    if (controller.playerInventory[i].noun == "canned laughter")
                    {
                        controller.playerInventory.RemoveAt(i);
                    }
                }
                controller.updateScore(2);
                break;
            case "bucket":
                if (controller.roomNavigation.currentRoom.rooomName == "Kitchen")
                {
                    if (isKitchenOnfire == true)
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
                        if (isHouseOnFire == true)
                        {
                            controller.updateScore(-2);
                            controller.LogStringWithReturn("It's too bad the rest of your house is on fire, because it relights the kitchen fire! If only you had done something sooner!");
                            isKitchenOnfire = true;
                        }
                        else { Kitchen.description = "A small burnt formerly teal colored kitchen, almost everything in the room is scorched or has burnt away."; }
                    }
                    else { controller.LogStringWithReturn("The bucket is already full of ice, so you arent sure what you would use it for at the moment."); }
                }
                else { controller.LogStringWithReturn("Try as you might you cant seem to think of a use for a bucket full of ice right now."); }
                break;
            case "phone": // In the final version I want to have a UI phone where you can dial numbers, maybe even some secret numbers. But thats a lot of work and not in scope right now
                controller.LogStringWithReturn("Cyberkat Cafe, Killer Kat speaking. Oh you are stuck in a text based adventure game? Have you tried using the Hint verb?, maybe it will help you. *click*");
                break;
            case "tv":
                isTVon = !isTVon;


                if (isTVon == true && isTvFixed == false)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, it's missing an antenna. It's currently just showing static yet something about it seems rather odd...";
                    controller.LogStringWithReturn("The TV crackles to life, but its just showing static.");
                }
                else if (isTVon == false && isTvFixed == false)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, it's missing an antenna. It's currently off yet something about it seems rather odd...";
                    controller.LogStringWithReturn("The TV shuts off with a flash, leaving nothing but a black screen.");
                }
                else if (isTVon == true && isTvFixed == true)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, you replaced it's antenna. It's currently just showing static yet something about it seems rather odd it almost resembles some kind of head.";
                    controller.LogStringWithReturn("The TV crackles to life, but its just showing static despite the antenna.");

                }
                else if (isTVon == false && isTvFixed == true)
                {
                    tv.examineDescription = "A small square purple colored CRT TV, you replaced it's antenna. It's currently off yet something about it seems rather odd it almost resembles some kind of head.";
                    controller.LogStringWithReturn("The TV shuts off with a flash, leaving nothing but a black screen.");
                    tvSecretCounter += 1;
                }
                if (tvSecretCounter == 5)
                {
                    controller.LogStringWithReturn("Suddely the TV starts playing a strange video.");
                    controller.LogStringWithReturn("Odd voice: Sometimes you're just steaming some hams when you stumble across something in the most unexpected of places, so today we're looking at the 5 most unexplained secrets in the Steamed Hams Text Based Adventure.");
                    controller.LogStringWithReturn("Odd voice: Number 1. The Backrooms some players reported that when moving between rooms of the house sometimes they were randomly teleported to the backrooms, a mysterious creepy pasta thats been making the rounds online.");
                    controller.LogStringWithReturn("Odd voice: Thanks to my friend the hacker EL BARTO for digging through the game files and finding that the player can be teleported here yourself by simply going weast.");
                    controller.LogStringWithReturn("Odd voice: Number 2. The Odd Hea *BZZT* Suddenly the TV goes back to static");
                }
                break;
            case "hanger":
                if (controller.roomNavigation.currentRoom.rooomName == "Living Room")
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
                if (isOvenOn == true) { controller.LogStringWithReturn("You turned the oven off."); oven.examineDescription = "A cheap white oven with a 4 burner stove and a broken timer. It is currently off."; } else { controller.LogStringWithReturn("You turned the oven on."); oven.examineDescription = "A cheap white oven with a 4 burner stove and a broken timer. It is currently on."; }
                isOvenOn = !isOvenOn;
                break;
            case "window":
                if (isWindowOpen == true) { controller.LogStringWithReturn("You close the window with a satisfying thud!"); window.examineDescription = "A large closed window that overlooks the Krustyburger, if you were into fitness this would be a good place to strech your calves."; controller.ResetRoomExits(Kitchen); controller.roomNavigation.UnpackExitsInRoom(); } else { controller.LogStringWithReturn("Its a bit heavy but you manage to open the window. The smell of fast food grease wafts in on the breeeze."); window.examineDescription = "A large open window, fast food grease permeates the air in your kitchen."; Kitchen.exits.Add(KrustyBurgerExit); }
                isWindowOpen = !isWindowOpen;
                break; //Please refrain from breaking the window
            case "z-remover":
                controller.secretNumber = 26;
                controller.LogStringWithReturn("You try to use the Z-Remover, however the letter remover finds no z in anything in the surrounding area.");
                controller.veryVerboseStatsText.text =  "Interesting, your Z-Remover has set the Secret Number to : " + controller.secretNumber;
                break;
        }
    }
    public void EndingManager(string endingKey)
    {
        switch (endingKey)
        {
            default:
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "You got the Error ending: There is a bug in the games code that caused the ending script to select an invalid ending. Seymour quits his job and becomes a computer programer, only to find his days are now consumed fixing bugs and reading documentation.");
                break;
            case "burningDeath":
                controller.displayText.color = Color.red;
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "You got the Burning Death ending: You and everyone else inside your house perish in the flaming inferno, if only you had put out the fire!");
                controller.persistentData.hasBurningDeathEnding = true; controller.persistentData.hasDied = true;
                break;
            case "chalmersLeaves":
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "You got the Chalmers Leaves ending: Chalmers is disgusted by your antics and leaves early. You should really be ashamed of yourself, acting like that.");
                controller.persistentData.hasChalmersLeavesEnding = true;
                break;
            case "insanity":
                break;
            case "speedrun":
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "Congradulations you got the Speedrun ending: Wow, you sure did finish the game really fast. Do you want to try actually playing now?");
                controller.persistentData.hasSpeedrunEnding = true;
                break;
            case "fired":
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "You got the Fired ending: You have lost your job aftering a frankly disastrous lunch. How are you going to recover from this?");
                controller.persistentData.hasFiredEnding = true;
                break;
        }
    }
}
