using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum StatType{LIFE,MANA,STRENGTH,CONSTITUTION,SPIRIT,MIND,CRIT_CHANCE,CRIT_MULT,ELEM_MULT,ATK_SPD,MOV_SPD,CAST_SPD};
public class CharacterStat 
{
    public float BaseValue;

    private bool isDirty = true;
    private float _value;
    private float lastBaseValue;

    public float Value{
        get {
            if(isDirty || lastBaseValue != BaseValue){ //evitar calcular toda vez, eficiencia
                lastBaseValue = BaseValue;
                isDirty = false; 
                _value = CalculateFinalValue();
            }
            return _value;
            }
        }

    public readonly ReadOnlyCollection<StatModifier> StatModifiers;
    private readonly List<StatModifier> statModifiers;

    public CharacterStat(float baseValue){
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public void AddModifier(StatModifier mod){
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }
    private int CompareModifierOrder(StatModifier a, StatModifier b){
        if(a.Order < b.Order)
            return -1;
        else if(a.Order > b.Order) 
            return 1;
        else
            return 0; //a == b
    }

    public bool RemoveModifier(StatModifier mod){
        if(statModifiers.Remove(mod)){
            isDirty = true;
            return true;
        }
        return false;
    }
    public bool RemoveAllModifiersFromSource(object source){
        bool didRemove = false;
        for(int i = statModifiers.Count -1; i >= 0; i--){
            if(statModifiers[i].Source == source){
                isDirty = true;
                didRemove = true;
                statModifiers.Remove(statModifiers[i]);
            }
        }
        return didRemove;
    }

    private float CalculateFinalValue(){

        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for(int i = 0; i < statModifiers.Count; i++){

            StatModifier mod = statModifiers[i];

            if(mod.Type == StatModType.Flat){
                finalValue += statModifiers[i].Value;
            }else if(mod.Type == StatModType.PercentAdd){
                sumPercentAdd += mod.Value;
                if(i+1 >= statModifiers.Count || statModifiers[i+1].Type != StatModType.PercentAdd){
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }else if(mod.Type == StatModType.PercentMult){
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Mathf.Round(finalValue);
    }
}
