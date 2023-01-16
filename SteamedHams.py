#Steamed hams but its an open source text based adventure game
#Surprisingly no one has done this before as far as I can tell
#Check out my podcast The CyberKat Cafe! We are avalible on all the best streaming platforms, for more info see cyberkatcafe.com
#Made by Killer Kat on github, January 2023
import random #lul so random XD 

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


    
#Global vars
score = 0
CurrentRoomID = 0
Inventory = []
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
#Rooms
defaultRoom = Room("Default Room", "A strikingly default room with a real sense of defaultness about it",0,)
defaultRoom.contents.append(defaultItem)
seriousRoom = Room("Serious Room", "An incredibly serious room, the most serious room you have ever seen",1)
seriousRoom.contents.append(seriousTable)


diningRoom = Room("Dining Room", "A small dining room with pastel blue walls, whover lives here has no sense of interior decorating.",2)
diningRoom.contents.append(diningRoomTable)
kitchen = Room("Kitchen", "A small square teal colored kitchen with a window overlooking a nearby fast food resturant. Its obvious whover lives here is not a very good cook.",3)
kitchen.contents.append(oven)
kitchen.contents.append(window)
livingRoom = Room("Living Room", "A cozy living room with pastel purple walls", 4)
livingRoom.contents.append(couch)
krustyBurger = Room("Krusty Burger", "A Krusty Burger resturant, it smells vaguely similar to the school kitchen that time you had to order grade F meat.",5)
#Room connections
defaultRoom.northRoom = seriousRoom
seriousRoom.southRoom = defaultRoom
diningRoom.northRoom = kitchen
diningRoom.eastRoom = livingRoom
livingRoom.westRoom = diningRoom
kitchen.southRoom = diningRoom
krustyBurger.southRoom = kitchen
#Need to define this after rooms or it doesnt work (wait or does it?)
currentRoom = diningRoom

def TextParser(text, room):
    global currentRoom
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
                        print("Nothing")
                    else:
                        for i in room.contents:
                            print(i.name)
                            if isinstance(i, Container):
                                print("It contains:")
                                if len(i.contents) == 0:
                                    print("Nothing")
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
                                    print("Nothing")
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
                    else: print("You cannot go north here.")
                    
                elif noun == "east" or noun == "e":
                    if room.eastRoom is not None:
                        currentRoom = room.eastRoom
                    else: print("You cannot go east here.")
                elif noun == "south" or noun == "s":
                    if room.southRoom is not None:
                        currentRoom = room.southRoom
                    else: print("You cannot go south here.")
                elif noun == "west" or noun == "w":
                    if room.westRoom is not None:
                        currentRoom = room.westRoom
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
    print("Score: " + str(score) + " " + promt)
    TextParser(input(">"), currentRoom)
    Main(currentRoom.name)

def Use(x):
    global isOvenOn
    global isWindowOpen
    match x:
        case "oven":
            isOvenOn = not isOvenOn
            if isOvenOn == False:
                oven.desc = "A cheap white oven with a 4 burner stove and a broken timer. It is currently off."
                print("Turned oven off.")
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

        case _:
            print("You cant use this.")

def ScoreHandler(x):
    global score
    score = score + x

def Hint():
    hintsList = ["Try going weast.", "XYZZY", "You cant get ye flask!", "You can get a hint by using the Hint verb!", "It's an open source game, just look at the code!", "Try calling our support hotline at 1-800-555-KILLERKAT", "Control alt delete", "Ask again later", "Have you listened to my podcast The CyberKat Cafe? Check out our website cyberkatcafe.com", "That's not a bug, it's a feature!"]
    print(random.choice(hintsList))
def Help():
    print("Look, use Look : Around to examine your surroundings or Look : Something to look at something in more detail, to see your inventory use Look : Inventory")
    print("Go, use Go and then one of the 4 cardinal directions to move in that direction. Provided there is something in that direction to move towards.")
    print("Take, use Take and then the name of an item to pick up that item, for items in containers you need to use Loot : Container Name")
    print("Drop, what do you think it does? use Drop : Item Name to drop an item in the current room.")
    print("Fill, used to put items inside of containers. use Fill : Container Name")
    print("There might be some other verbs, but I'll give you a Hint and say they might not be as useful as you would hope.")

Main("Wellcome! To give commands use the format VERB: NOUN, the : is required. Try Help : Please for a list of commands")

