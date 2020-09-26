using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State_AddStateToAttacks", menuName = "States/Apply On Attack", order = 0)]
public class AddStateToAttacks : State
{
    public float buffDuration = 5;
    public State stateToApply;
    public Color changeEntityColor;
    public Color defaultColor;
    public SkillType onlyForTypeAttacks = SkillType.None;

    public override void InitState(EntityStats receiver,EntityStats applier = null)
    {
        StateStack ss = receiver.GetStack(this);
        if(ss == null){
            ss = new StateStack(this, buffDuration,1);
            receiver.states.Add(ss);
        }else{
            ss.ResetTimer();
        }
        if(receiver.entityGraphic){
            receiver.entityGraphic.color = changeEntityColor;
        }
    }

    public override void OnDealDamage(EntityStats dealer, EntityStats target, DamageSource damageInfo = null){
        if((damageInfo != null && damageInfo.skillType == onlyForTypeAttacks) || onlyForTypeAttacks == SkillType.None)
            target.ApplyState(stateToApply);
    }

    public override void ConcludeState(EntityStats receiver){
        if(receiver.entityGraphic){
            receiver.entityGraphic.color = defaultColor;
        }
    }
}
