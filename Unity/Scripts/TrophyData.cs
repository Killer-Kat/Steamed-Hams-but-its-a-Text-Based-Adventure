using UnityEngine;

[CreateAssetMenu(fileName = "NewTrophy", menuName = "Trophies/Trophy Data")]
public class TrophyData : ScriptableObject
{
    [Header("Basic Info")]
    public string trophyName;
    [TextArea]
    public string trophyDescription;

    [Header("Icons")]
    public Sprite unlockedIcon;
    public Sprite lockedIcon;

    [Header("State")]
    public bool unlocked;

    [Header("Optional")]
    public string unlockHint;   // shown when locked
    public bool secretTrophy;   // hides name/description until unlocked
}
