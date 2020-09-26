using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State_Stun", menuName = "States/Stun", order = 0)]
public class StunState : State
{
    public float stunDuration = 3;

    public override void InitState(EntityStats receiver, EntityStats applier = null)
    {
        StateStack ss = receiver.GetStack(this);
        if(ss == null){
            ss = new StateStack(this, stunDuration,1);
            receiver.states.Add(ss);
        }else{
            ss.ResetTimer();
        }
        /* receiver.canMove = false;
        SkillUser su = receiver.GetComponent<SkillUser>();
        if(su){
            su.canCastSkills = false;
        } */
        SkillUser su = receiver.GetComponent<SkillUser>();
        if(su){
            su.canCastSkills = false;
        } 
        receiver.stunned = true;
    }

    public override void ConcludeState(EntityStats receiver){
        receiver.stunned = false;
        SkillUser su = receiver.GetComponent<SkillUser>();
        if(su){
            su.canCastSkills = true;
        } 
    }
}
