using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings", menuName = "Player/Character Settings", order = 0)]
public class PlayerCharacterSettings : ScriptableObject
{
    public Race solarRace;
    public Race lunarRace;
    public Race ascendantRace;
    
    [Header("Currency")]
    public int crystals = 0;
    public int coins = 0;
    
    [Header("Map and Upgrades")]
    public Race[] astralMapRaces = new Race[8];
    public Upgrade[] possiblePermanentUpgrades;
}
