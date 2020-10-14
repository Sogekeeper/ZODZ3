using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Settings", menuName = "Player/Character Settings", order = 0)]
public class PlayerCharacterSettings : ScriptableObject
{
    public Race[] possibleRaces;

    public Race solarRace;
    public Race lunarRace;
    public Race ascendantRace;
    
    [Header("Currency")]
    public int crystals = 0;
    public int coins = 0;
    
    [Header("Map and Upgrades")]
    public Race[] astralMapRaces = new Race[8];
    public Upgrade[] possiblePermanentUpgrades;

    [Header("Augments")]
    public AugmentOption[] augmentsPicked = new AugmentOption[8];

    public void LoadData(){
        //carregar cristais
        
    }

    public void SetupCharacter(){
        coins = 0;
        ResetAugments();
        //setar vida
    }

    [ContextMenu("DEBUG - Reset Augments Only")]
    public void ResetAugments(){
        if(possibleRaces == null) return;
        for (int i = 0; i < possibleRaces.Length; i++)
        {
            possibleRaces[i].ResetAugments();
        }
        augmentsPicked = new AugmentOption[8];
    }

    [ContextMenu("DEBUG - Reset Character")]
    public void DebugResetCharacter(){
        ResetAugments();
        astralMapRaces = new Race[8];
        crystals = 0;
        coins = 0;
        augmentsPicked = new AugmentOption[8];
    }

}
