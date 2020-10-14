using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State_DamageOverTime", menuName = "States/DoT State", order = 0)]
public class DoTState : State
{
    public enum PercentageType{
        Life, Strength, Mind, Spirit, Constitution
    };
  public int maxStacks = 1;
  public float damageAmountPercentage = 0.05f;
  public Multiplier damageMultiplier;
    public PercentageType percentageType;
  public float initialDuration = 3;
  public Multiplier durationMultiplier;
  public float initialTickRate = 0.8f;
  //public float durationReduction = 0.0f;

  public override void InitState(EntityStats receiver, EntityStats applier = null)
  {
    StateStack ss = receiver.GetStack(this);
    if (ss == null)
    {
      ss = new StateStack(this, initialDuration*durationMultiplier.GetValue(), initialTickRate, 1, applier);
      receiver.states.Add(ss);
    }
    else
    {
      if (ss.stackAmount < maxStacks)
      {
        ss.stackAmount++;
        //ss.stateDuration *= durationReduction;
      }
      else
      {
        ss.ResetTimer();
      }
    }
  }

  public override void TickState(EntityStats receiver, StateStack stack)
  {
    float totalDamage = damageAmountPercentage;
    for (int i = 1; i < stack.stackAmount; i++)
    {
      //totalDamage += totalDamage * damagePercentIncreasePerStack;
    }
    if(percentageType == PercentageType.Life)    
        totalDamage = totalDamage * receiver.totalLife.Value;
    else if(percentageType == PercentageType.Strength)    
        totalDamage = totalDamage * stack.stateOrigin.strength.Value;
    else if(percentageType == PercentageType.Mind)    
        totalDamage = totalDamage * stack.stateOrigin.mind.Value;
    else if(percentageType == PercentageType.Spirit)    
        totalDamage = totalDamage * stack.stateOrigin.spirit.Value;
    else if(percentageType == PercentageType.Constitution)    
        totalDamage = totalDamage * stack.stateOrigin.constitution.Value;
    
    totalDamage *= damageMultiplier.GetValue();
    if(totalDamage < 1) totalDamage = 1;
    receiver.TakeDamage((int)totalDamage);
    
  }
}
