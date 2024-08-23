using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData
{
    //Game Progression
    public bool hasCompletedFirstLoop { get; set; } = false;
    public int NumberOfTimeLoops { get; set; } = 0;

    public bool hasDied { get; set; } = false; //spoooooky I know 

    //Endings
    public bool hasBurningDeathEnding = false;
    public bool hasChalmersLeavesEnding = false;
    public bool hasInsanityEnding = false;
    public bool hasSpeedrunEnding = false;

    //Settings

    //Stats
}
