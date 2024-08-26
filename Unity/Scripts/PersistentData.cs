using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData
{
    //Game Progression
    public bool hasCompletedFirstLoop { get; set; } = false;
    public int NumberOfTimeLoops { get; set; } = 0;

    public bool hasDied { get; set; } = false; //spoooooky I know 

    
    //Trophies
    public bool hasOddHeadTrophy = false; //Fixed the TV
    public bool hasSteamedHamsTrophy = false; //Followed the og lunch script
    public bool hasXYZZYTrophy = false; //Say the magic word
    //Trophy for dying, can just use existing bool
    public bool hasZRemoverTrophy = false; //find and use the Z-Remover
    public bool hasHitchHikersGuideTrophy = false; //Find and use the HitchHikers Guide to the Galaxy.
    

    //Endings
    public bool hasBurningDeathEnding = false;
    public bool hasChalmersLeavesEnding = false;
    public bool hasInsanityEnding = false;
    public bool hasSpeedrunEnding = false;
    public bool hasFiredEnding = false;
    public bool hasBackroomsEnding = false;
    public bool hasClosedDoorEnding = false;

    //Settings

    //Stats
    public int numberOfDeaths = 0;
}
