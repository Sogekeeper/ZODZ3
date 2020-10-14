using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Augment_Option", menuName = "Skills/Augments/Augment Option", order = 1)]
public class AugmentOption : ScriptableObject
{
    //quando jogador escolher, apenas aplicar mudança nos multiplicadores, não é necessário fazer mudanças toda cena

    public string augmentName = "Hate";
    [TextArea] public string augmentDescription = "Roar stuns for longer.";
    public Multiplier[] multipliers;

    public void ResetMultipliers(){
        if(multipliers == null) return;
        for (int i = 0; i < multipliers.Length; i++)
        {
            if(multipliers[i] == null) continue;
            multipliers[i].Reset();
        }
    }

    public void UpgradeMultipliers(){
        if(multipliers == null) return;
        for (int i = 0; i < multipliers.Length; i++)
        {
            if(multipliers[i] == null) continue;
            multipliers[i].ChangeValue(1);
        }
    }
    
}
