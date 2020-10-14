using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State_Bleeding", menuName = "States/Bleeding State", order = 0)]
public class BleedingState : State
{
  public int maxStacks = 3;
  public bool percentDamage = true;
  public float damageAmount = 0.05f;
  public float damagePercentIncreasePerStack = 0.5f;
  public Multiplier bleedDamageMultiplier;
  public float initialDuration = 3;
  public Multiplier bleedDurationMultiplier;
  public float initialTickRate = 0.8f;
  public float durationReduction = 0.75f;

  public override void InitState(EntityStats receiver, EntityStats applier = null)
  {
    StateStack ss = receiver.GetStack(this);
    if(ss == null){
      ss = new StateStack(this, initialDuration*bleedDurationMultiplier.GetValue(),initialTickRate,1,applier);
      receiver.states.Add(ss);
    }else{
      if(ss.stackAmount < maxStacks){
        ss.stackAmount++;
        ss.stateDuration *= durationReduction;
      }else{
        ss.ResetTimer();
      }
    }
  }

  public override void TickState(EntityStats receiver, StateStack stack){
    float totalDamage = damageAmount;
    for(int i = 1; i < stack.stackAmount; i++){
      totalDamage += totalDamage * damagePercentIncreasePerStack;
    }
    if(percentDamage){
      totalDamage = totalDamage * stack.stateOrigin.strength.Value;
    }
    totalDamage *= bleedDamageMultiplier.GetValue();
    if(totalDamage < 1) totalDamage = 1;
      receiver.TakeDamage((int)totalDamage);
  }
}
