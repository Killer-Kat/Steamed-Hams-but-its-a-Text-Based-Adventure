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
ovenKitchenFireCountdown = 11
kitchenFireSpreadCountdown = 5
burningHouseDeathCountdown = 5
isKitchenOnFire = False
isHouseOnFire = False
isChalmersWaitingOutsideForFire = False
chalmersKitchenCheckDone = False
isOvenOn = True
isWindowOpen = False
isSteamedHams = False
isTVon = False
TVsecretCounter = 0
isTVfixed = False

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
comboMeal = Item("Combo Meal", "A takeout container with the Krusty Burger combo meal #4. It's 4 hamburgers and 2 large fries. You should really think about putting this on a nice serving tray before having lunch.", True, "combomeal")
steamedHams = Item("Steamed Hams", "Steamed Hams just like they make them in Albany, it's really just fast food on a fancy platter but you're trying your best.", True)
phone = Item("Phone", "A white wall mounted landline phone, you can Use this to make calls.", False, "phone")
apron = Item("Apron", "A white lace lined cooking apron.", True)
Inventory.append(apron)
tv = Item("TV","A small square purple colored CRT TV, it's missing an antenna. It's currently off yet something about it seems rather odd...",False, "tv")
hanger = Item("Hanger", "A metal coat hanger, the kind you get for free at the dry cleaners. On your salary its the only kind you can afford.", True, "hanger")
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
livingRoom.contents.append(phone)
livingRoom.contents.append(tv)
porch = Room("Front Porch", "A small front porch with a banister steps, there are two large windows through which you can see your living room and dining room",6)
lawn = Room("Lawn", "Your lawn, the grass is well cut and the air is fresh as can be! Which is not particulary fresh considering the grease in the air from the Krusty Burger next door.",7)
lawn.contents.append(hanger)
krustyBurger = Room("Krusty Burger", "A Krusty Burger resturant, it smells vaguely similar to the school kitchen that time you had to order grade F meat.",5)
krustyBurger.contents.append(jeremy)
backRooms = Room("Back Rooms","A stale yellow office building, damp carpet squishes beneath your feet. The ever-present hum of florescent lights makes you feel a deep unease.",-1)
#Room connections
defaultRoom.northRoom = seriousRoom
seriousRoom.southRoom = defaultRoom
diningRoom.northRoom = kitchen
diningRoom.eastRoom = livingRoom
diningRoom.southRoom = porch
livingRoom.westRoom = diningRoom
kitchen.southRoom = diningRoom
krustyBurger.southRoom = kitchen
krustyBurger.eastRoom = lawn
porch.northRoom = diningRoom
porch.southRoom = lawn
backRooms.northRoom = backRooms
backRooms.southRoom = backRooms
backRooms.eastRoom = backRooms
backRooms.westRoom = backRooms
defaultRoom.westRoom = backRooms
lawn.northRoom = krustyBurger
lawn.southRoom = porch
#Need to define this after rooms or it doesnt work (wait or does it?)
currentRoom = diningRoom

def TextParser(text, room):
    global currentRoom
    global CurrentRoomID
    try:
        split1 = text.split(":")
        verb = split1[0].strip().lower()
        noun = split1[1].strip().lower()
        
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
                            else:
                                 print("You dont have a " + x)
                            
                    else: print("Container not found, try Fill : Container Name")
            case "go": 
                curse = random.randrange(0,500,1)
                if curse == 13:
                    noun = "weast"
                
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
                elif noun == "weast":
                    print("You noclip through reality")
                    currentRoom = defaultRoom
                    CurrentRoomID = 0
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
    global burningHouseDeathCountdown
    global chalmersKitchenCheckDone
    if isOvenOn == True:
        ovenKitchenFireCountdown -= 1
        if CurrentRoomID == 3:
            if(ovenKitchenFireCountdown) <= 6 and chalmersKitchenCheckDone == False:
                chalmersKitchenCheckDone = True
                HAMS(6)
        if ovenKitchenFireCountdown == 0:
            HAMS(2)
    if isKitchenOnFire == True:
        kitchenFireSpreadCountdown -= 1
        if kitchenFireSpreadCountdown == 0:
            HAMS(3)
    if isHouseOnFire == True:
        burningHouseDeathCountdown -= 1
        if burningHouseDeathCountdown == 0:
            HAMS(7)
    print("Score: " + str(score) + " " + promt)
    TextParser(input(">"), currentRoom)
    Main(currentRoom.name)

def Use(x):
    global isOvenOn
    global isWindowOpen
    global isKitchenOnFire
    global isHouseOnFire
    global isTVon
    global TVsecretCounter
    global isTVfixed
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
                else : kitchen.desc = "A small burnt formerly teal colored kitchen, almost everything in the room is scorched or has burnt away."
            else: print("You have no use for a bucket right now.")
        case "combomeal":
            if CurrentRoomID == 3:
                print("You put the meal on a serving platter.")
                Inventory.remove(comboMeal)
                Inventory.append(steamedHams)
            else: print("If you were in the kitchen you could put this on a nice serving platter.")
        case "phone":
            print("1. call Fire Department.")
            print("2. call 1-800-555-KILLERKAT")
            a = input(">")
            if a == str(1):
                if isHouseOnFire or isKitchenOnFire:
                    print("Carl Carlson: Springfield Volunteer Fire Department Carl Speaking, oh Seymour how are ya?")
                    print("Carl Carlson: A fire at your place, We'll be there right away!")
                    HAMS(8)
                else: 
                    print("Calling the fire department without a fire is a serious crime you know.")
                    ScoreHandler(-1)
            elif a == str(2):
                print('Cyberkat Cafe, Killer Kat speaking. Oh you are stuck in a text based adventure game? Have you tried using "Hint : Please", maybe it will help you. *click*')
            else: print("Hello? I think you have a wrong number. *click*")

        case "tv":
            isTVon = not isTVon
            if isTVon == True and isTVfixed == False:
                tv.desc = "A small square purple colored CRT TV, it's missing an antenna. It's currently just showing static yet something about it seems rather odd..."
                print("The TV crackles to life, but its just showing static.")
                if isTVfixed == True:
                    TVsecretCounter += 1
            elif isTVon == False and isTVfixed == False: 
                tv.desc = "A small square purple colored CRT TV, it's missing an antenna. It's currently off yet something about it seems rather odd..."
                print("The TV shuts off with a flash, leaving nothing but a black screen.")
            elif isTVon == True and isTVfixed == True:
                tv.desc = "A small square purple colored CRT TV, you replaced it's antenna. It's currently just showing static yet something about it seems rather odd it almost resembles some kind of head."
                print("The TV crackles to life, but its just showing static despite the antenna.")
                if isTVfixed == True:
                    TVsecretCounter += 1
            elif isTVon == False and isTVfixed == True:
                tv.desc = "A small square purple colored CRT TV, you replaced it's antenna. It's currently off yet something about it seems rather odd it almost resembles some kind of head."
                print("The TV shuts off with a flash, leaving nothing but a black screen.")
    
            if TVsecretCounter == 5:
                print("Suddely the TV starts playing a strange video.")
                print("Odd voice: Sometimes you're just steaming some hams when you stumble across something in the most unexpected of places, so today we're looking at the 5 most unexplained secrets in the Steamed Hams text based adventure.")
                print("Odd voice: Number 1. The Backrooms some players reported that when moving between rooms of the house sometimes they were randomly teleported to the backrooms, a mysterious copy pasta thats been making the rounds online.")
                print("Odd voice: Thanks to my friend the hacker EL BARTO for digging through the game files and finding that the player can be teleported here yourself by simply going weast.")
                print("Odd voice: Number 2. The Odd Hea *BZZT* Suddenly the TV goes back to static")
        case "hanger":
            if CurrentRoomID == 4:
                Inventory.remove(hanger)
                isTVfixed = True
                print("You use the coat hanger to make a new TV antenna.")
            else: print("You dont use the hanger, an odd thought pops into your head that you might need it for something else.")


        case _:
            print("You cant use this.")

def ScoreHandler(x):
    global score
    score = score + x
def PersonalityHandler(type, x):
    global oddPoints
    global politePoints
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
        print("Jermy: Hi wellcome to Krusty Burger home of the Rib Wich, now with extra verisimilitude! what can I get you?")
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
    global isSteamedHams
    global ovenKitchenFireCountdown
    global chalmersKitchenCheckDone
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
                    EndGame()
                    
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
        chalmersKitchenCheckDone = True #So chalmers wont walk into the kitchen while the house burns down.
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
        if isSteamedHams == True:
            print(chalmers.name +": But you steam a good ham.")
        EndGame()
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
            elif i.name.lower() == "combo meal":
                PersonalityHandler("odd",2)
                PersonalityHandler("polite",-2)
                ScoreHandler(1)
                print(chalmers.name + ": Seymore this is a bag of Krusty Burger, I thought you were cooking dinner?")
                print("1. I burnt my roast so I got a replacement.")
                print("2. What? No I made all this myself.")
                print("3. I thought you liked Krusty Burger?")
                print("4. Oh you must have misheard me, I said I was buying something tasty not frying something tasty.")
                c = input(">")
                if c == str(1):
                    ScoreHandler(1)
                    PersonalityHandler("odd",1)
                    PersonalityHandler("polite",1)
                    print(chalmers.name +": Ah I see, but are you sure you couldnt have made something else?")
                elif c == str(2):
                    ScoreHandler(-3)
                    PersonalityHandler("odd",1)
                    PersonalityHandler("polite",-2)
                    print(chalmers.name +": Seymour, this food is all still in the Krusty Burger packaging, you're clearly lying to me.")
                elif c == str(3):
                    ScoreHandler(1)
                    PersonalityHandler("odd",2)
                    print(chalmers.name +": I do, but I thought you were cooking us lunch?")
                elif c == str(4):
                    ScoreHandler(1)
                    PersonalityHandler("odd",1)
                    PersonalityHandler("polite",1)
                    print(chalmers.name +": Ah of course, my mistake.")
            elif i.name.lower() == "steamed hams":
                ScoreHandler(4)
                print("Seymour: Superintendent I hope you're ready for some mouth watering hamburgers!")
                if isSteamedHams == True:
                    print(chalmers.name +": I thought we were having steamed clams?")
                    print('1. Oh no, I said "steamed hams". Thats what I call hamburgers.')
                    print("2. I burnt the clams so I bought some hamburgers instead.")
                    print("3. We are, thats what I call hamburgers.")
                    print("4. You must have misheard me, maybe get a hearing aid old man.")
                    d = input(">")
                    if d == str(1):
                        ScoreHandler(1)
                        PersonalityHandler("odd",1)
                        print(chalmers.name +': You call hamburgers "steamed hams"?')
                        print('1. Yes, its a regional dialect.')
                        print("2. Yes, its jarhead slang I picked up durring my time in Vietnam.")
                        print("3. No, I call them steamed clams.")
                        e = input(">")
                        if e == str(1):
                            ScoreHandler(1)
                            PersonalityHandler("odd",1)
                            print(chalmers.name +': Uh-huh. Eh, what region?')
                            print("1. Upstate New York.")
                            print("2. Springfield.")
                            print("3. Ohio River Valley.")
                            print("4. The Moon.")
                            f = input(">")
                            if f == str(1):
                                print(chalmers.name +": Really?  Well I'm from Utica and I've never heard anyone use the phrase steamed hams.")
                                ScoreHandler(-1)
                                print("1. Oh, not in Utica, no. It's an Albany expression.")
                                print("2. Well maybe you should have listened harder stupid.")
                                print("3. Uhh... Lets change the conversation.")
                                g = input(">")
                                if g == str(1):
                                    ScoreHandler(2)
                                    print(chalmers.name +": I see.")
                                elif g == str(2):
                                    ScoreHandler(-2)
                                    PersonalityHandler("polite",-2)
                                    print(chalmers.name +": What the hell Seymour, you know I have half a mind to fire you!")
                                elif g == str(3):
                                    ScoreHandler(-1)
                                    print(chalmers.name +": Uh, I guess.")

                            elif f == str(2):
                                print(chalmers.name +": Seymour what are you on about? We both live in Springfield and I've never heard anyone use the phrase steamed hams.")
                                ScoreHandler(-2)
                                PersonalityHandler("polite"-2)
                                PersonalityHandler("odd",2)
                            elif f == str(3):
                                ScoreHandler(2)
                                print(chalmers.name +": Ah I see.")
                            elif f == str(4):
                                ScoreHandler(-4)
                                PersonalityHandler("odd",4)
                                print(chalmers.name +": What? Seymour are you feeling ok? Perhaps you should lay down.")
                        elif e == str(2):
                            ScoreHandler(2)
                            PersonalityHandler("odd",1)
                            print(chalmers.name +": Mmm, explains why I've never heard anyone use the phrase before.")
                        elif e == str(3):
                            print(chalmers.name +": You... What, but I? Nevermind.")
                            ScoreHandler(-1)
                            PersonalityHandler("odd",3)
                            
                    elif d == str(2):
                        ScoreHandler(-1)
                        print(chalmers.name +': You burnt them with steam? That takes talent.')
                    elif d == str(3):
                        ScoreHandler(-1)
                        PersonalityHandler("odd",4)
                        print(chalmers.name +': You call hamburgers "steamed clams"? Thats very strange.')
                    elif d == str(4):
                        ScoreHandler(-4)
                        PersonalityHandler("polite"-4)
                        print(chalmers.name +": What? How dare you!")
                print(chalmers.name +": You know, these hamburgers are quite similar to the ones they have at Krusty Burger.")
                print("1. Hohoho, no! Patented Skinner Burgers. Old family recipe!")
                print("2. They are from Krusty Burger.")
                print("3. Yes, I make them like this because I love Krusty Burgers.")
                h = input(">")
                if h == str(1) and isSteamedHams == True:
                    ScoreHandler(1)
                    PersonalityHandler("odd",1)
                    print(chalmers.name +": For steamed hams?")
                    print("1. Yes.")
                    print("2. No.")
                    i = input(">")
                    if i == str(1):
                        ScoreHandler(1)
                        print(chalmers.name +": Yes, and you call them steamed hams, despite the fact they are obviously grilled?")
                        print("Seymour: Y- Uh.. you know, the... One thing I should... excuse me for one second.")
                        print(chalmers.name +": Of course.")
                    else: print(chalmers.name +": But you just said... Nevermind."); ScoreHandler(-2)
                elif h == str(1):
                    print(chalmers.name +": Well maybe you and Krusty share ancestors then.")
                elif h == str(2):
                    ScoreHandler(-1)
                    PersonalityHandler("polite",-1)
                    print(chalmers.name +": You said you were making lunch today. I should have know better than to expect anything from you Seymour.")
                elif h == str(3):
                    ScoreHandler(2)
                    PersonalityHandler("odd",1)
                    print(chalmers.name +": That's strange but I will admit, I do like a good Krusty Burger every now and then.")

                      

        print(chalmers.name + ": Well I should be going")
        if isOvenOn == True:
            isHouseOnFire = True
            ovenKitchenFireCountdown = -1
            HAMS(2)
        else: HAMS(4)

    elif x == 6: #Chalmers checks the kitchen
        print("Chalmers enters the room")
        if isWindowOpen == True:
            print("Superintendent! I was just... uh, just stretching my calves on the windowsill. Isometric exercise! Care to join me?")
            PersonalityHandler("odd",2)
        print(chalmers.name +": Why is there smoke coming out of your oven Seymour?")
        print("1. Thats not smoke, its steam! Steam from the steamed clams we're having. Mmm steamed clams!")
        print("2. My oven timer is broken and I burnt my roast.")
        print("3. It's smoke from the smoked ham we're having!")
        print("4. Uh, I was just smoking a cigarette in here and I use the oven as an ash tray.")
        a = input(">")
        if a == str(1):
            ScoreHandler(2)
            isSteamedHams = True
            print("Chalmers frowns and returns to the dining room.")
        elif a == str(2):
            ScoreHandler(1)
            PersonalityHandler("polite",1)
            print(chalmers.name +": I hope you have something else you can make for lunch then.")
            print("Chalmers frowns and returns to the dining room.")
        elif a == str(3):
            ScoreHandler(2)
            print(chalmers.name +": Well I do like smoked ham, I'll take my seat then.")
            print("Chalmers returns to the dining room.")
        elif a == str(4):
            ScoreHandler(-2)
            PersonalityHandler("odd",4)
            PersonalityHandler("polite",-2)
            print(chalmers.name +": I what... Well I hope you didnt cook lunch in there then.")
            print("Chalmers frowns and returns to the dining room.")
    elif x == 7: #House burns down
        print("Your house has burnt down killing everyone inside.")
        print("You are dead.")
        EndGame()
    elif x == 8: #Fire Department puts out fire.
        isKitchenOnFire = False
        isHouseOnFire = False
        kitchen.desc = "A small burnt formerly teal colored kitchen, almost everything in the room is scorched or has burnt away."
        ScoreHandler(2)
        print("The Springfield Volunteer Fire Department shows up and extinguishes the infurno that is your house.")
        print("You really should have known better than to start a house fire considering you are a member of the Fire Department.")



def Hint():
    hintsList = ["Try going weast.", "XYZZY", "You cant get ye flask!", "You can get a hint by using the Hint verb!", "It's an open source game, just look at the code!", "Try calling our support hotline at 1-800-555-KILLERKAT", "Control alt delete", "Ask again later", "Have you listened to my podcast The CyberKat Cafe? Check out our website cyberkatcafe.com", "That's not a bug, it's a feature!"]
    print(random.choice(hintsList))

def EndGame():
    print("Game over you scored: " + str(score) + " points.")
    print("Your personality was " + str(oddPoints) + " Odd and " + str(politePoints) + " Polite.")
    print("Game by Killer Kat, if you liked this check out my other projects at cyberkatcafe.com")
def Help():
    print("Look, use Look : Around to examine your surroundings or Look : Something to look at something in more detail, to see your inventory use Look : Inventory")
    print("Go, use Go and then one of the 4 cardinal directions to move in that direction. Provided there is something in that direction to move towards.")
    print("Take, use Take and then the name of an item to pick up that item, for items in containers you need to use Loot : Container Name")
    print("Drop, what do you think it does? use Drop : Item Name to drop an item in the current room.")
    print("Fill, used to put items inside of containers. use Fill : Container Name")
    print("Use, can be used on some items to interact with them. try Use : Item")
    print("Talk, use it to talk to people, be sure to use their full name. Talk : Person")
    print("There might be some other verbs, but I'll give you a Hint and say they might not be as useful as you would hope.")
HAMS(1)
Main("Wellcome! To give commands use the format VERB: NOUN, the : is required. Try Help : Please for a list of commands")

