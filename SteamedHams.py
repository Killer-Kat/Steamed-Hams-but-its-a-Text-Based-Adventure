#Steamed hams but its an open source text based adventure game
#Surprisingly no one has done this before as far as I can tell
#Check out my podcast The CyberKat Cafe! We are avalible on all the best streaming platforms, for more info see cyberkatcafe.com
#Made by Killer Kat on github, January 2023
import random

class Room:
    def __init__(self, name, desc, roomID, northRoom=None, eastRoom=None, southRoom=None, westRoom=None):
        self.name = name
        self.desc = desc
        self.roomID = roomID
        self.contents = []
        #These are the rooms that are connected to this room, IE southRoom is the room to the south of this one
        self.northRoom = northRoom
        self.eastRoom = eastRoom
        self.southRoom = southRoom
        self.westRoom = westRoom

        
    
    def __str__(self):
        return f"{self.name}: {self.desc}"

class Item:
    def __init__(self, name, desc, canTake, useAction="Default"):
        self.name = name
        self.desc = desc
        self.canTake = canTake
        self.useAction = useAction
    def __str__(self):
        return f"{self.name}: {self.desc}"

class Container(Item):
    def __init__(self, name, desc, canTake, useAction="Default"):
        super().__init__(name,desc, canTake, useAction)
        
        self.contents = []

class Person:
    def __init__(self, name, desc, dialogueID, canTake=False, useAction="person"):
        self.name = name
        self.desc = desc
        self.dialougeID = dialogueID
        self.useAction = useAction
        self.canTake = canTake



    
#Global vars
score = 0
CurrentRoomID = 2
Inventory = []
oddPoints = 0
politePoints = 0
rudePoints = 0
ovenKitchenFireCountdown = 7
kitchenFireSpreadCountdown = 5
isKitchenOnFire = False
isHouseOnFire = False
isChalmersWaitingOutsideForFire = False
isOvenOn = True
isWindowOpen = False

#Items
defaultItem = Item("Default Item", "An incredibly default item, you bask in the glow of its defaultness!", True)
seriousItem = Item("Serious Item", "An incredibly serious item, you feel the aura of its seriousness!", True)
seriousTable = Container("Serious Table", "The most serious table you have ever seen!", False)
seriousTable.contents.append(seriousItem)
diningRoomTable = Container("Dining Room Table", "A small circular dining room table with two chairs and a white table cloth.", False)
couch = Item("Couch", "An olive green couch with a dent in the cushion indicating someone spends a lot of time sitting here", False)
oven = Container("Oven", "A cheap white oven with a 4 burner stove and a broken timer. It is currently on.", False, "oven")
burntRoast = Item("Burnt Roast", "An expensive roast that someone ruined by burning it.",True)
oven.contents.append(burntRoast)
window = Item("Window", "A large closed window that overlooks the Krustyburger, if you were into fitness this would be a good place to strech your calves.", False, "window")
wine = Item("Wine", "A bottle of 1982 Sauvignon Blanc, this would pair well with seafood.", True)
bucket = Container("Bucket", "A metal champagne bucket, it was a gift from mother.", True, "bucket")
bucket.contents.append(wine)
diningRoomTable.contents.append(bucket)
wineGlasses = Item("Wine Glasses", "Glass Glasses for wine, how fancy! put them on the table if you want to serve wine.", True)
ribwich = Item("Ribwich", 'A rib themed sandwich, the box proudly proclaims "Now without lettuce!" you question if its a good idea to eat this.', True)
comboMeal = Item("Combo Meal", "A Krusty Burger combo meal #4. It's 4 hamburgers and 2 large fries.", True, "combomeal")
steamedHams = Item("Steamed Hams", "Steamed Hams just like they make them in Albany, it's really just fast food on a fancy platter but you're trying your best.", True)
#People
chalmers =  Person("Chalmers", 'Your boss, the Superintendent you had better be sure to impress him after your latest blunder with the "Minimalist" classroom layouts.',0)
jeremy = Person("Jeremy Freedman", "Krusty Burger employee with the name tag Jeremy. A tired looking teen with a pimple coverd face and a high pitched voice",2)
#Rooms
defaultRoom = Room("Default Room", "A strikingly default room with a real sense of defaultness about it",0,)
defaultRoom.contents.append(defaultItem)
seriousRoom = Room("Serious Room", "An incredibly serious room, the most serious room you have ever seen",1)
seriousRoom.contents.append(seriousTable)

diningRoom = Room("Dining Room", "A small dining room with pastel blue walls, whover lives here has no sense of interior decorating.",2)
diningRoom.contents.append(diningRoomTable)
diningRoom.contents.append(chalmers)
kitchen = Room("Kitchen", "A small square teal colored kitchen with a window overlooking a nearby fast food resturant. Its obvious whover lives here is not a very good cook.",3)
kitchen.contents.append(oven)
kitchen.contents.append(window)
kitchen.contents.append(wineGlasses)
livingRoom = Room("Living Room", "A cozy living room with pastel purple walls", 4)
livingRoom.contents.append(couch)
porch = Room("Front Porch", "A small front porch with a banister steps, there are two large windows through which you can see your living room and dining room",6)
krustyBurger = Room("Krusty Burger", "A Krusty Burger resturant, it smells vaguely similar to the school kitchen that time you had to order grade F meat.",5)
krustyBurger.contents.append(jeremy)
#Room connections
defaultRoom.northRoom = seriousRoom
seriousRoom.southRoom = defaultRoom
diningRoom.northRoom = kitchen
diningRoom.eastRoom = livingRoom
diningRoom.southRoom = porch
livingRoom.westRoom = diningRoom
kitchen.southRoom = diningRoom
krustyBurger.southRoom = kitchen
porch.northRoom = diningRoom
#Need to define this after rooms or it doesnt work (wait or does it?)
currentRoom = diningRoom

def TextParser(text, room):
    global currentRoom
    global CurrentRoomID
    try:
        split1 = text.split(":")
        verb = split1[0].strip().lower()
        noun = split1[1].strip().lower()
        #print(verb, noun) #for debug, del this line later
        match verb:
            case "look":
                if noun.lower() == "around": ### what? it shoud load the room by default and also if you specify it.
                    print("You see " + room.desc + " and : ")
                    if len(room.contents) == 0:
                        print(" Nothing")
                    else:
                        for i in room.contents:
                            print(i.name)
                            if isinstance(i, Container):
                                print("It contains:")
                                if len(i.contents) == 0:
                                    print(" Nothing")
                                else:
                                    for c in i.contents:
                                        print(" " + c.name)
                    if room.northRoom is not None:
                        print("To the north there is: " + room.northRoom.name)
                    if room.eastRoom is not None:
                        print("To the east there is: " + room.eastRoom.name)
                    if room.southRoom is not None:
                        print("To the south there is: " + room.southRoom.name)
                    if room.westRoom is not None:
                        print("To the west there is: " + room.westRoom.name)
                elif noun.lower() == "inventory":
                    print("You have:")
                    for i in Inventory:
                        print(i.name)
                else: 
                    parserLookHint = 0
                    for i in room.contents:
                        if noun == i.name.lower() :
                            print(i.desc)
                            parserLookHint = 1
                            if isinstance(i, Container):
                                print("It contains:")
                                if len(i.contents) == 0:
                                    print(" Nothing")
                                else:
                                    for c in i.contents:
                                        print(" " + c.name)
                            break
                    for i in Inventory:
                        if noun == i.name.lower():
                            print(i.desc)
                            parserLookHint = 1
                            break
                    if parserLookHint == 0:
                         print("Look at what? To look at your surroundings use Look : Around")
                         print("to check your inventory use Look : Inventory")
            case "take":
                for i in room.contents:
                    if noun == i.name.lower():
                        if i.canTake == True :
                            room.contents.remove(i)
                            Inventory.append(i)
                            print("You take the " + i.name)
                        else: print ("You cant take the " + i.name)
            case "drop":
                for i in Inventory:
                    if noun == i.name.lower():
                        Inventory.remove(i)
                        room.contents.append(i)
                        print("You drop the " + i.name)
            case "loot":
                for i in room.contents:
                    if noun == i.name.lower():
                        x = input("Loot what from " + noun + "? ")
                        for y in i.contents:
                            if y.name.lower() == x.lower() :
                                i.contents.remove(y)
                                Inventory.append(y)
                                print("You take the " + x + " from the " + noun)
                            else: print(x + " not found in this container.")
                            break
                        break
                    else: print("Container not found, try Loot : Container Name")
            case "fill":
                for i in room.contents:
                    if noun == i.name.lower():
                        x = input("Fill " + noun + " with what? ")
                        for y in Inventory:
                            if y.name.lower() == x.lower() :
                                i.contents.append(y)
                                Inventory.remove(y)
                                print("You put the " + x + " in the " + noun)
                            else: print("You dont have a " + x)
                            break
                    else: print("Container not found, try Fill : Container Name")
            case "go": 
                if noun == "north" or noun == "n":
                    if room.northRoom is not None:
                         currentRoom = room.northRoom
                         CurrentRoomID = room.northRoom.roomID
                    else: print("You cannot go north here.")
                    
                elif noun == "east" or noun == "e":
                    if room.eastRoom is not None:
                        currentRoom = room.eastRoom
                        CurrentRoomID = room.eastRoom.roomID
                    else: print("You cannot go east here.")
                elif noun == "south" or noun == "s":
                    if room.southRoom is not None:
                        currentRoom = room.southRoom
                        CurrentRoomID = room.southRoom.roomID
                    else: print("You cannot go south here.")
                elif noun == "west" or noun == "w":
                    if room.westRoom is not None:
                        currentRoom = room.westRoom
                        CurrentRoomID = room.westRoom.roomID
                    else: print("You cannot go west here.")
            case "use":
                usehint = True
                for i in room.contents:
                    if i.name.lower() == noun:
                        Use(i.useAction)
                        usehint = False
                        break
                for i in Inventory:
                    if i.name.lower() == noun:
                        Use(i.useAction)
                        usehint = False
                        break
                if usehint == True:
                    print("Could not find " + noun + " try Use : Item.")
            case "talk":
                for i in room.contents:
                    if i.name.lower() == noun and isinstance(i,Person):
                        Talk(i.dialougeID)
            case "hint":
                Hint()
            case "help":
                if noun == "please":
                    print("Thank you for being polite.")
                    Help()
                else:
                    print("You should be more polite!")
            case "xyzzy":
                print("A hollow voice says, Nerd!")
            case _: 
                print("The parser didnt recognize your verb, please try again!")
    except IndexError:
        print("Oops! The Parser didnt understand Try again...")
    
  
def Main(promt):
    global ovenKitchenFireCountdown
    global kitchenFireSpreadCountdown
    if isOvenOn == True:
        ovenKitchenFireCountdown -= 1
        if ovenKitchenFireCountdown == 0:
            HAMS(2)
    if isKitchenOnFire == True:
        kitchenFireSpreadCountdown -= 1
        if kitchenFireSpreadCountdown == 0:
            HAMS(3)
    print("Score: " + str(score) + " " + promt)
    TextParser(input(">"), currentRoom)
    Main(currentRoom.name)

def Use(x):
    global isOvenOn
    global isWindowOpen
    global isKitchenOnFire
    global isHouseOnFire
    match x:
        case "person":
            print("Its not nice to try and use people. Maybe try Talk : Person")
        case "oven":
            isOvenOn = not isOvenOn
            if isOvenOn == False:
                oven.desc = "A cheap white oven with a 4 burner stove and a broken timer. It is currently off."
                print("Turned oven off.")
                if isKitchenOnFire == True:
                    print("Its a little late for that to help much considering you kitchen is currently on fire. But at least you remembered to turn it off! ")
            else: 
                oven.desc = "A cheap white oven with a 4 burner stove and a broken timer. It is currently on."
                print("Turned oven on!")
        case "window":
            isWindowOpen = not isWindowOpen
            if isWindowOpen == True:
                print("You open the window.")
                kitchen.northRoom = krustyBurger
                window.desc = "A large open window that overlooks the Krustyburger, if you were into fitness this would be a good place to strech your calves."

            else: 
                print("You close the window.")
                kitchen.northRoom = None
                window.desc = "A large closed window that overlooks the Krustyburger, if you were into fitness this would be a good place to strech your calves."
        case "bucket":
            if CurrentRoomID == 3 and isKitchenOnFire == True:
                bucket.contents.clear()
                isKitchenOnFire = False
                ScoreHandler(4)
                print("You dump the bucket out and extinguish the fire!")
                if isHouseOnFire == True:
                    print("It's too bad the rest of your house is on fire, because it relights the kitchen fire! If only you had done something sooner!")
                    ScoreHandler(-2)
                    isKitchenOnFire = True
            else: print("You have no use for a bucket right now.")
        case "combomeal":
            if CurrentRoomID == 3:
                print("You put the meal on a serving platter.")
                Inventory.remove(comboMeal)
                Inventory.append(steamedHams)
            else: print("If you were in the kitchen you could put this on a nice serving platter.")

        case _:
            print("You cant use this.")

def ScoreHandler(x):
    global score
    score = score + x
def PersonalityHandler(type, x):
    global oddPoints
    global politePoints
    global rudePoints
    if type == "odd":
        oddPoints += x
    elif type == "polite":
        politePoints += x
def Talk(x):
    if x == 0:
        print(chalmers.name +": Ah Seymore are you ready to serve lunch?")
        a = input(">").lower()
        if a == "yes" or a == "y":
            b = input("Serve what? >").lower() #I forget the () here and spent 10 minutes trying to debug it
            for i in Inventory:
                if i.name.lower() == b:
                    diningRoomTable.contents.append(i)
                    Inventory.remove(i)
            chalmers.dialougeID = 1
            HAMS(5)
        elif a == "no" or a == "n":
            print(chalmers.name +": Alright, let me know when you're ready.")
        else: print("What was that? I didnt quite hear you, let me know when you're ready to serve lunch."); PersonalityHandler("odd",1)
    elif x == 2:
        print("Jermy: Hi wellcome to Krusty Burger home of the Rib Witch, now with extra verisimilitude! what can I get you?")
        print("1. The #4 family combo meal")
        print("2. I'll have 2 number 9s, a number 9 large, a number 6 with extra dip, a number 7, 2 number 45s one with cheese, and a large soda.")
        print("3. One Ribwich please!")
        print("4.Nothing I changed my mind.")
        a = input(">")
        if a == str(1):
            print("Ok sir here you go!")
            ScoreHandler(2)
            Inventory.append(comboMeal)
            jeremy.dialougeID = 3
        elif a == str(2):
            print("Uh, I didnt get all that and our soda machine is broken, so here's 2 number 9s and a number 7")
            ScoreHandler(2)
            Inventory.append(comboMeal)
            Inventory.append(ribwich)
            jeremy.dialougeID = 3
        elif a == str(3):
            print("MmmHmm *hands you a Ribwich*")
            ScoreHandler(1)
            Inventory.append(ribwich)
            jeremy.dialougeID = 3
        elif a == str(4):
            print("Uh ok sir, well i'll be here if you change your mind again.")
    else: print("They cant talk now.")

def HAMS(x): #H.A.M.S Hastly Asembled Management Script
    global isKitchenOnFire
    global isHouseOnFire
    global currentRoom
    global CurrentRoomID
    global isChalmersWaitingOutsideForFire
    #Intro scene
    if x == 1:
        print("*DING DONG* You open the front door, its your boss Superintendent Chalmers. You have invited him over for a lunch to try and impress him after your latest blunder.")
        print("Chalmers: Well Seymore I made it, despite your directions.")
        print("1. Ah Superintendent Chalmers wellcome, I hope you're prepaired for an unforgetable luncheon!")
        print("2. Ah Chalmers my old friend just in time, how is Shauna doing?")
        print("3. Hi Super Nintendo Chalmers")
        print("4. Please leave. *Shuts door*")
        a = input("Choose an option: ")
        if a == str(1):
            print("Chalmbers: Mmyeah")
            PersonalityHandler("odd",1)
            PersonalityHandler("polite",1)
            ScoreHandler(1)
        elif a == str(2):
            print("Chalmers: I wish you hadnt asked, she got fired again after she got caught smoking in the breakroom.")
            PersonalityHandler("polite",-2)
            ScoreHandler(-2)
        elif a == str(3):
            print("Super Nintendo Chalmers: What? Ah nevermind.")
            PersonalityHandler("odd",3)
            ScoreHandler(-1)
            chalmers.name = "Super Nintendo Chalmers"
            chalmers.desc = 'Your boss, the Super Nintendo you had better be sure to impress him after your clearly just forget his name...'
        elif a == str(4):
            print("Chalmers: SEYMOREEEEEE!")
            input("Congradulations you have reached the speedrun ending. Press enter to quit.")
            quit # did not work in testing. why?
        else: print("Not a valid choice, input a number from 1-4"); HAMS(1)
    elif x == 2: #Kitchen on fire
        isKitchenOnFire = True
        ScoreHandler(-10)
        if CurrentRoomID == 3:
            print("Suddenly the fire in your oven spreads to the rest of your kitchen. You really should have turned that off!")
        elif CurrentRoomID == 2:
            print("Smoke starts to pour from your kitchen doorframe, you excuse yourself and quickly pop into the kitchen to find that its currently on fire! You pop back into the dining room and hear: ")
            print(chalmers.name + ": Good Lord what is happening in there!")
            print("1. Aurora Borealis")
            print("2. A fire has broken out, you head outside and I'll get help!")
            print("3. Nothing, everything is fine")
            print("4. What where?")
            a = input("Choose an option: ")
            if a == str(1):
                ScoreHandler(1)
                PersonalityHandler("odd",2)
                print(chalmers.name + ": Aurora Borealis!? At this time of year? At this time of day? In this part of the country? Localized entirely within your kitchen?")
                b = input(chalmers.name + ": May I see it? >").lower()
                if b == "yes" or b == "y":
                    currentRoom = kitchen
                    CurrentRoomID = 3
                    diningRoom.contents.remove(chalmers)
                    print(chalmers.name + ": What the hell Seymore, this isnt the Aurora Borealis your kitchen is on fire! I'm getting the hell out of here!")
                    ScoreHandler(-10)
                    PersonalityHandler("polite",1)
                    
                elif b == "no" or b == "n":
                    ScoreHandler(2)
                    PersonalityHandler("odd",2)
                    diningRoom.contents.remove(chalmers)
                    HAMS(4)
                else: 
                    print("What? I asked a simple yes or no question, I'm out of here!")
                    PersonalityHandler("odd",3)
                    diningRoom.contents.remove(chalmers)
                    ScoreHandler(-1)

            elif a == str(2) :
                ScoreHandler(3)
                PersonalityHandler("polite",2)
                print(chalmers.name + ": Good thinking Seymore, I would call for help but I think Shauna took my cellphone again!")
                diningRoom.contents.remove(chalmers)
                porch.contents.append(chalmers)
                isChalmersWaitingOutsideForFire = True
            elif a == str(3) : 
                ScoreHandler(1)
                PersonalityHandler("odd",1)
                PersonalityHandler("polite",1)
                print(chalmers.name + ": Really? Well I should be going.")   
                diningRoom.contents.remove(chalmers)
                HAMS(4)
            elif a == str(4) :
                ScoreHandler(-2)
                PersonalityHandler("odd",2)
                PersonalityHandler("polite",-2)
                print(chalmers.name +": Your kitchen you dolt, it looks like its on fire! I'm getting out of here!")
                diningRoom.contents.remove(chalmers)
                porch.contents.append(chalmers)
                isChalmersWaitingOutsideForFire = True


        
        kitchen.desc = "A small square teal colored kitchen, its somewhat hard to make out any other details due to the fact that it is currently on fire!"
    elif x == 3: #House on fire
        isHouseOnFire = True
        print("Mother: Seymore the house is on fire!")
    elif x == 4: #Chalmers goodbye
        currentRoom = porch
        CurrentRoomID = 6
        if politePoints <= 6 and oddPoints > 6:
            print(chalmers.name +": Well Seymore I must say you are an odd fellow.")
            ScoreHandler(1)
        elif politePoints > 6 and oddPoints > 6:
            print(chalmers.name +": Well Seymore I must say you are an odd yet polite fellow.")
            ScoreHandler(3)
        elif politePoints <= 0 and oddPoints > 6:
            print(chalmers.name +": Well Seymore I must say you are a rude and odd man.")
            ScoreHandler(-5)
        elif politePoints > 6 and oddPoints < 6:
            print(chalmers.name +": Well Seymore I must say you are a polite fellow.")
            ScoreHandler(4)
        elif politePoints <= 0 and oddPoints < 6:
            print(chalmers.name +": Well Seymore I must say you are a rude jerk!")
            ScoreHandler(-4)
        else: print(chalmers.name +": Well Seymore I must say you are a boring drag.")
    elif x == 5: #Serving lunch
        for i in diningRoomTable.contents:
            if i.name.lower() == "burnt roast":
                PersonalityHandler("odd",5)
                PersonalityHandler("polite",-5)
                print(chalmers.name + ": Seymore this roast is burnt. What happend?")
                print("1. I burnt it by mistake")
                print("2. I'ts not burnt, its just well done!")
                print("3. It was mothers fault, she made it!")
                print("4. This is just how I like it.")
                a = input(">")
                if a == str(1):
                    print(chalmers.name + ": I see, well thats a shame")
                    PersonalityHandler("polite",1)
                elif a == str(2):
                    print(chalmers.name + ": If you say so...")
                    PersonalityHandler("polite"-1)
                    PersonalityHandler("odd",1)
                elif a == str(3):
                    print(chalmers.name + ": I thought you were making lunch today Seymore?")
                    PersonalityHandler("polite",-5)
                elif a == str(4):
                    print(chalmers.name + ": Really? Well you certainly have strange tastes.")
                    PersonalityHandler("odd",5)
            elif i.name.lower() == "wine glasses":
                print(chalmers.name + ":Ah I see you brought out the wine glasses, let me open the vintage. its a 1982 Sauvignon Blanc I picked up in New York while visting family.")
                PersonalityHandler("polite",2)
            elif i.name.lower() == "ribwich":
                ScoreHandler(1)
                print(chalmers.name +": Seymore is that a Ribwich from Krusty Burger?")
                print("1. Yes, I thought you might like it")
                print("2. No, It's a home made Rib Sandwich, old family recipe")
                print("3. I have no idea what you're talking about *Quickly eat Ribwich*")
                b = input(">")
                if b == str(1):
                    print(chalmers.name +": Oh well, thanks I guess?")
                    ScoreHandler(1)
                    PersonalityHandler("odd",3)
                    PersonalityHandler("polite",1)
                elif b == str(2):
                    print(chalmers.name +": What are you talking about? Its still in the box, I can clearly see it's from Krusty Burger!")
                    ScoreHandler(-3)
                    PersonalityHandler("odd",3)
                    PersonalityHandler("polite",-4)
                elif b == str(3):
                    print(chalmers.name +": Did you just..? What? Nevermind.")
                    ScoreHandler(-1)
                    PersonalityHandler("odd",5)
                    PersonalityHandler("polite",-1)




        print(chalmers.name + ": Well I should be going")
        HAMS(4)



def Hint():
    hintsList = ["Try going weast.", "XYZZY", "You cant get ye flask!", "You can get a hint by using the Hint verb!", "It's an open source game, just look at the code!", "Try calling our support hotline at 1-800-555-KILLERKAT", "Control alt delete", "Ask again later", "Have you listened to my podcast The CyberKat Cafe? Check out our website cyberkatcafe.com", "That's not a bug, it's a feature!"]
    print(random.choice(hintsList))
def Help():
    print("Look, use Look : Around to examine your surroundings or Look : Something to look at something in more detail, to see your inventory use Look : Inventory")
    print("Go, use Go and then one of the 4 cardinal directions to move in that direction. Provided there is something in that direction to move towards.")
    print("Take, use Take and then the name of an item to pick up that item, for items in containers you need to use Loot : Container Name")
    print("Drop, what do you think it does? use Drop : Item Name to drop an item in the current room.")
    print("Fill, used to put items inside of containers. use Fill : Container Name")
    print("Use, can be used on some items to interact with them. try Use : Item")
    print("There might be some other verbs, but I'll give you a Hint and say they might not be as useful as you would hope.")
HAMS(1)
Main("Wellcome! To give commands use the format VERB: NOUN, the : is required. Try Help : Please for a list of commands")

