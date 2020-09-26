using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_BasicStatProgression", menuName = "States/Upgrades/Basic Stat Progression", order = 0)]
public class BasicStatUpgrade : Upgrade
{
    public float[] values;
    public StatModType targetModType;
    public StatType targetType;

    private StatModifier addedMod;

    public override void SetDescriptionText(TextMeshProUGUI text)
    {
        if(amount == maxAmount){
            text.text = string.Format(upgradeDescription, values[amount-1],"(Max)");
        }else if(amount == 0){
            text.text = string.Format(upgradeDescription, 0,values[amount]);
        }else{
            text.text = string.Format(upgradeDescription, values[amount-1],values[amount]);
        }
    }

    public override void InitState(EntityStats receiver, EntityStats applier = null)
    {
        if(amount <= 0 || amount > maxAmount) return;
        StateStack ss = new StateStack(this, -1, -1, 1, applier);
        receiver.states.Add(ss);
        addedMod = new StatModifier(values[amount-1],targetModType,this);
        if(targetType == StatType.STRENGTH){
            receiver.strength.AddModifier(addedMod);
        }
        if(targetType == StatType.MIND){
            receiver.mind.AddModifier(addedMod);
        }
    }

    public override void ConcludeState(EntityStats receiver)
    {
        base.ConcludeState(receiver);
        if(targetType == StatType.STRENGTH){
            if(!receiver.strength.RemoveModifier(addedMod))Debug.Log("WARNING: no buff removed");
        }
        if(targetType == StatType.MIND){
            receiver.mind.RemoveAllModifiersFromSource(this);
        }
    }
}
