using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HAMS : MonoBehaviour //H.A.M.S Hastly Asembled Management Script
{
    public GameController controller;

    public DialogueObject IntroDobj;
    public bool isIntroComplete = false;
    public DialogueObject PoliteGoodbyeDobj;
    public int chalmersIntroCountdown = 10;//how many turns chalmers will wait before leaving if skinner does not open the door.
    public Room DiningRoom;
    public Room Porch;

    public bool didChalmersEnterHouse = false; //so we don't start the CEK scene if he is outside + I want to add a thing later where you exit via window and meet him on the porch
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
    public int lostInTheBackroomsCountdown = 6; //countdown of how long you can stay in the backrooms before you get the backrooms ending.
    public int poisonCountdown = 10; //countdown of how long you have until you die from eating posion.
    public bool isPoisoned = false;
    public InteractableObject vomit;
    public bool triggerPostLunchGoodbye = false;

    public bool toiletHasKey = true;
    public InteractableObject KrustyBurgerToilet;
    public InteractableObject rustyKey;
    public InteractableObject tv;
    public bool isTVon = false;
    public int tvSecretCounter = 0;
    bool isTvFixed = false;

    public InteractableObject table;
    [SerializeField]
    public DialogueObject LunchGrossFoodDobj;

    public Person chalmers;
    public Person jermey;

    public InteractableObject combomeal;
    public InteractableObject ribwich;
    public DialogueObject krustyburgerbreak;

    public Room Bedroom;
    public Room Kitchen;
    public Exit KrustyBurgerExit;
    public DialogueObject KitchenFireDobj;
    public InteractableObject window;
    public InteractableObject bdwindow;
    public bool isWindowOpen = false;
    public bool isBDWindowOpen = false;
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
        DiningRoom.peopleInRoom.Add(chalmers);
        didChalmersEnterHouse = true;
    }

    public void ResetPoison()
    {
        isPoisoned = false;
        poisonCountdown = 10;
        controller.LogStringWithReturn("You are no longer poisoned.");
        controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(vomit);
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
        if (isPoisoned)
        {
            poisonCountdown -= 1;
            if(controller.health >= 10)
            {
                controller.UpdateHealth(-5);
            }
        }
        if (controller.roomNavigation.currentRoom.rooomName == "Backrooms")
        {
            lostInTheBackroomsCountdown -= 1;
            if(lostInTheBackroomsCountdown == 0)
            {
                EndingManager("backrooms");
            }
        }
        if (isIntroComplete == false)
        {
            chalmersIntroCountdown -= 1;
            if(chalmersIntroCountdown == 0)
            {
                EndingManager("closeddoor");
            }else if (chalmersIntroCountdown == 9)
            {
                controller.LogStringWithReturn("You hear a Knock at your front door. You should: Open Front Door");
            }
            else
            {
                controller.LogStringWithReturn("You hear a Knock at your front door.");
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
        if (didChalmersEnterHouse == true)
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
        controller.DisplayLoggedText();
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
        if (triggerPostLunchFire == false && table.contents.Count == 0)
        {
            triggerPostLunchGoodbye = true; //I need to implement logic so that its not just lunch then goodbye but this will work for now
        }

        // Pre-scan table for context so bucket/wine glass logic is no longer order-dependent
        bool hasWineGlasses = false;
        bool hasBucket = false;
        bool hasGrossFood = false;

        int foodCount = 0;

        for (int scan = 0; scan < table.contents.Count; scan++)
        {
            if (table.contents[scan].noun == "wine glasses") hasWineGlasses = true;
            if (table.contents[scan].noun == "bucket") hasBucket = true;
            if (table.contents[scan].isGrossFood == true) hasGrossFood = true;

            // Count food items
            if (table.contents[scan].canBeEaten == true)
                foodCount++;
        }
        
        // If there is gross food on the table you get scolded.
        if (hasGrossFood == true)
        {
            controller.dialogueController.UnpackFromDialogueObject(LunchGrossFoodDobj);
            controller.updateScore(-2); // optional penalty
            // Remove all gross food so the logic doesn't loop forever
            for (int g = table.contents.Count - 1; g >= 0; g--) 
            { if (table.contents[g].isGrossFood == true) table.contents.RemoveAt(g); }
        }


        // Dictionary for food logic (keeps your comments and structure intact)
        Dictionary<string, Action> foodActions = new Dictionary<string, Action>
    {
        {
            "steamed hams", () =>
            {
                controller.updateScore(5);
                controller.dialogueController.UnpackFromDialogueObject(LunchSteamedHamsDobj);
                didChalmersEat = true;
            }
        },
        {
            "hamburgers", () =>
            {
                controller.updateScore(5);
                controller.dialogueController.UnpackFromDialogueObject(LunchHamburgersDobj);
                didChalmersEat = true;
            }
        },
        {
            "combo meal", () =>
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchComboMealDobj);
                didChalmersEat = true;
            }
        },
        {
            "ribwich", () =>
            {
                controller.updateScore(1);
                controller.dialogueController.UnpackFromDialogueObject(LunchRibwichDobj);
                //Not going to have this count as chalmers eating, since you have the option to eat it yourself and I don't want to program that edge case right now.
            }
        },
        {
            "burnt roast", () =>
            {
                controller.updateScore(-1);
                controller.UpdateOddPoints(5);
                controller.UpdatePolitePoints(-5);
                didChalmersEat = true;
                controller.dialogueController.UnpackFromDialogueObject(LunchRoastDobj); //I am going to make a note here that I changed order that the dialogue controller unpacks dialogue objects so it runs the HAMS commands last so that I could get this to work right. Honestly it was driving me crazy, but thankfully I had my programmer socks on and was able to realize that I made the entire thing so I could just change it to work how I wanted. They really do make you better at coding! :3
            }
        },
        {
            "perfect roast", () =>
            {
                controller.updateScore(2);
                controller.UpdatePolitePoints(2);
                didChalmersEat = true;
                controller.dialogueController.UnpackFromDialogueObject(LunchPerfectRoastDobj); //Sadly I do not have any cute femboy/trans girl clothes on right now because its single digit temps outside and I am wearing layers on layers since my house does not have heating. 
            }
        },
        {
            "steamed clams", () =>
            {
                controller.updateScore(2);
                controller.UpdatePolitePoints(2);
                controller.dialogueController.UnpackFromDialogueObject(LunchSteamedClamsDobj);
                didChalmersEat = true;
            }
        },
        {
            "pickled herring", () =>
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchHerringDobj);
            }
        },
        {
            "apple", () =>
            {
                controller.updateScore(1);
                controller.dialogueController.UnpackFromDialogueObject(LunchAppleDobj);
                didChalmersEat = true;
            }
        }
    };

        // We will remove items AFTER the loop to avoid mutating the list while iterating
        InteractableObject itemToRemove = null;

        for (int i = 0; i < table.contents.Count; i++)
        {
            
            if (table.contents[i].noun == "wine glasses")
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchWineGlassDobj);
                wineGlassesUsed = true;
                itemToRemove = table.contents[i];
                break;
            }
            else if (table.contents[i].noun == "bucket" && wineGlassesUsed == false && hasWineGlasses == false) //fixed logic: now checks actual presence of wine glasses, not index order
            {
                controller.dialogueController.UnpackFromDialogueObject(LunchBucketDobj);
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(IceBucket);
                itemToRemove = table.contents[i];
                break;
            }
            else if (table.contents[i].noun == "bucket" && (wineGlassesUsed == true || hasWineGlasses == true))  //still need to move the bucket off the table so the fire triggers, but dont want chalmers to scold the player for not having wine glasses.
            {
                controller.roomNavigation.currentRoom.InteractableObjectsInRoom.Add(IceBucket);
                itemToRemove = table.contents[i];
                break;
            }

            // Food logic via dictionary
            if (foodActions.ContainsKey(table.contents[i].noun))
            {
                foodActions[table.contents[i].noun].Invoke();
                itemToRemove = table.contents[i];
                break;
            }
        }

        // Remove processed item safely
        if (itemToRemove != null)
            table.contents.Remove(itemToRemove);

        if (triggerPostLunchFire == true)//this one should be last as if true it will put us in the post lunch fire scene.
        {
            //KitchenOnFire();
            controller.dialogueController.UnpackFromDialogueObject(KitchenFireDobj); //hard coding this instead of calling the method becuase it breaks otherwise. ¯\_(.-.)_/¯ 
            isKitchenOnfire = true;
            Kitchen.description = "A small square teal colored kitchen, its somewhat hard to make out any other details due to the fact that it is currently on fire!";
            Debug.Log("post lunch fire triggered");
        }
        else if (triggerPostLunchGoodbye == true)
        {
            controller.dialogueController.UnpackFromDialogueObject(PoliteGoodbyeDobj);
            Debug.Log("post lunch goodbye triggered");
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
                chalmers.description = "Your boss, the Super Nintendo is here you had better be sure to impress him after you clearly just forget his name...";
                //chalmers.name = "Super Nintendo Chalmers"; //CUrrently Breaks the talk command and I cannot be bothered to fix it atm
                isIntroComplete = true;
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
            case "plgoodbye":
                Porch.peopleInRoom.Add(chalmers);
                DiningRoom.peopleInRoom.Clear(); //dont write code like this, if there were another charater in the dining room this would also clear them.
                controller.roomNavigation.AttemptToChangeRooms("south"); //also dont write code like this, jesus
                chalmersGoodbye();
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
            case "steamedHamsTrophy":
                controller.persistentData.hasSteamedHamsTrophy = true;
                break;
            case "introcomplete":
                isIntroComplete = true;
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
                if (isWindowOpen == true) { controller.LogStringWithReturn("You close the window with a satisfying thud!"); window.examineDescription = "A large closed window that overlooks the Krustyburger, if you were into fitness this would be a good place to strech your calves."; controller.ResetRoomExits(Kitchen); } //controller.roomNavigation.UnpackExitsInRoom(); } 
                else { controller.LogStringWithReturn("Its a bit heavy but you manage to open the window. The smell of fast food grease wafts in on the breeeze."); window.examineDescription = "A large open window, fast food grease permeates the air in your kitchen."; Kitchen.exits.Add(KrustyBurgerExit); }
                isWindowOpen = !isWindowOpen;
                break; //Please refrain from breaking the window
            case "bdwindow":
                if (isBDWindowOpen == true) { controller.LogStringWithReturn("You close the window with a satisfying thud!"); bdwindow.examineDescription = "Your faithful bedroom window sits there closed iluminating the room, despite having both curtains and blinds."; controller.ResetRoomExits(Bedroom); } 
                else { controller.LogStringWithReturn("Its a bit stuck but you manage to open the window. The smell of fresh cut grass and burger grease wafts in on the breeeze."); bdwindow.examineDescription = "Your faithful bedroom window is sitting there wide open, if you were into fitness this would be a terrible place to strech your calves"; Bedroom.exits.Add(KrustyBurgerExit); }
                isBDWindowOpen = !isBDWindowOpen;
                break; //Please refrain from breaking this window as well
            case "z-remover":
                controller.persistentData.hasZRemoverTrophy = true;
                controller.secretNumber = 26;
                controller.LogStringWithReturn("You try to use the Z-Remover, however the letter remover finds no z's to remove from anything in the surrounding area.");
                controller.veryVerboseStatsText.text =  "Interesting, your Z-Remover has set the Secret Number to : " + controller.secretNumber; //This gets overwritten because it happens before the game updates the secret number via the usual method. 
                break;
            case "guide":
                controller.persistentData.hasHitchHikersGuideTrophy = true;
                controller.LogStringWithReturn("You try to press the butons in vain but they are stuck together, however you can read the current entry Earth: Mostly Harmless.");
                break;
            case "frontdoor":
                if(isIntroComplete == false)
                {
                    IntroScene();
                }
                else
                {
                    controller.LogStringWithReturn("You diligently open the front door, only to be greated by a vast expanse of empty air.");
                    for (int i = 0; i < controller.roomNavigation.currentRoom.peopleInRoom.Count; i++)
                    {
                        if(controller.roomNavigation.currentRoom.peopleInRoom[i].name == "Chalmers")
                        {
                            controller.UpdateOddPoints(1);
                            controller.LogStringWithReturn("*Chalmers notices you opening and closing the front door for no apparent reason.*");
                        }
                    }
                }
                break;
            case "bed":
                controller.LogStringWithReturn("You decide to lay down on your bed and nap for a breif moment... for some reason.");
                break;
            case "sink":
                controller.LogStringWithReturn("You splash some cold water on your face and remind yourself to breathe. You will get through this.");
                break;            
            case "kbtoilet":
                if (toiletHasKey)
                {
                    controller.LogStringWithReturn("You reach into the grimy toilet and pull out... a rusty key. Hope it was worth it.");
                    controller.updateScore(5);
                    controller.UpdateHealth(-5);
                    toiletHasKey = false;
                    controller.playerInventory.Add(rustyKey);
                    KrustyBurgerToilet.examineDescription = "Further examination reveals nothing worthwhile.";
                    KrustyBurgerToilet.description = "Unsuprisingly there is a toilet here, surprisingly you just stuck your hand down it.";
                }
                else
                {
                    controller.LogStringWithReturn("You consider reaching back into the grimy toilet, and then resolutely decide not to and instead simply flush it.");
                }
                break;
        }
    }

    public void EatActionTree(string eatingkey)
    {
        switch (eatingkey)
        {
            default:
                break;
            case "apple":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", hopefully this will deter any doctors from approaching you.");
                controller.updateScore(1);
                controller.UpdateHunger(10);
                controller.UpdateHealth(2);
                break;
            case "burnt roast":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", and you have no idea why, you aren't having lunch and its burnt to a crisp.");
                controller.updateScore(-4);
                controller.UpdateHunger(25);
                controller.UpdateHealth(-5);
                break;
            case "perfect roast":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", I guess you will need to find something else to serve to chalmers.");
                controller.updateScore(-5);
                controller.UpdateHunger(25);
                break;            
            case "steamed clams":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", and they are perfectly steamed, I guess you will need to find something else to serve to chalmers.");
                controller.updateScore(-5);
                controller.UpdateHunger(25);
                break;
            case "hamburgers":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", and you feel a momentary sadness as you wonder what in your life has lead you to sneak away from lunch to buy several peoples worth of fast food and then eat it all by yourself on your finest serving platter.");
                controller.updateScore(-5);
                controller.UpdateHunger(25);
                controller.UpdateHealth(-5);
                break;            
            case "combo meal":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", and you feel a momentary sadness as you wonder what in your life has lead you to sneak away from lunch to buy several peoples worth of fast food and then eat it all by yourself.");
                controller.updateScore(-5);
                controller.UpdateHunger(25);
                controller.UpdateHealth(-5);
                break;            
            case "steamed hams":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", and you for a moment consider if you should stop the lies, man up and come clean about your mistakes. Then you remember you are a coward and now need to find something else to serve for lunch.");
                controller.updateScore(-5);
                controller.UpdateHunger(25);
                controller.UpdateHealth(-5);
                break;

            case "ribwich":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", its as delicous as it is unhealthy.");
                controller.updateScore(2);
                controller.UpdateHunger(15);
                controller.UpdateHealth(-10);
                break;
            case "milk":
                controller.LogStringWithReturn("The " + eatingkey + ", is so old its formed into a nearly solid mass which you foolishly force down your gullet. Right after as if in protest to this atrocity your guts start to hurt. You have been poisoned.");
                controller.updateScore(-10);
                controller.UpdateHunger(10);
                controller.UpdateHealth(-5);
                isPoisoned = true;
                break;
            case "pickled herring":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", and then remeber why you had left it in the fridge so long. Gross.");
                controller.updateScore(-5);
                controller.UpdateHunger(10);
                break;
            case "vomit":
                controller.LogStringWithReturn("What the hell is wrong with you? are you a dog!? You for some unknown reason eat your " + eatingkey + ", before getting a crash course in why not to do this as your body protests. You have been poisoned... Idiot.");
                controller.updateScore(-20);
                controller.UpdateHunger(10);
                controller.UpdateOddPoints(10);
                isPoisoned = true;
                break;
            case "rusty key":
                controller.LogStringWithReturn("You eat the " + eatingkey + ", you found in a toilet. That certainly was *a* choice. You have been poisoned.");
                controller.updateScore(-10);
                controller.UpdateHunger(10);
                controller.UpdateHealth(-25);
                isPoisoned = true;
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
                controller.persistentData.hasBurningDeathEnding = true; controller.persistentData.hasDied = true; controller.persistentData.numberOfDeaths += 1;
                break;
            case "poisonDeath":
                controller.displayText.color = Color.green;
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "You got the Poisoned Death ending: You ate poison and died, I hope you are happy with youself, who will look after your mother now?");
                controller.persistentData.hasPoisonedEnding = true; controller.persistentData.hasDied = true; controller.persistentData.numberOfDeaths += 1;
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
            case "backrooms":
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "You got the Backrooms ending: After being lost in the Backrooms for days, you eventually succumb to hunger. Right before you closed your eyes for the last time, you could have sworn you saw something moving in the darkness.");
                controller.persistentData.hasBackroomsEnding = true; controller.persistentData.hasDied = true; controller.persistentData.numberOfDeaths += 1;
                break;
            case "closeddoor":
                controller.ShowEndGamePopup(controller.score, controller.oddPoints, controller.politePoints, "You got the Closed Door ending: After getting no response after knocking repeatedly chalmers gives up and assumes skinner is not home, he procedes to the Krusty Burger next door to get lunch.");
                controller.persistentData.hasClosedDoorEnding = true;
                break;
        }
    }
}
