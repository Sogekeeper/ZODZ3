using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_InstantBuff", menuName = "Skills/Instant Buff", order = 3)]
public class InstantBuff : Skill
{
    public State buffToApply;
    public RaceDependingAnimSet castingAnimSet;
    [Header("Optional")]
    public PoolObject stillGraph;
    public PoolObject castEffect;

    public override void FrameUpdate(SkillUser user)
    {
    }

    public override bool Initialize(SkillUser user)
    {
        if(castingAnimSet)
            user.ReplaceSkillAnimationSet(castingAnimSet.GetSetForRace(user.userStats.baseRace));
        user.userStats.rb.velocity = Vector3.zero;
        user.userStats.canMove = false;
        user.userAnim.Play("Skill",0,0);
        user.ComputeSkill(this);
        user.userStats.ChangeMana(-skillCost);
        return true;
    }

    public override void InterruptSkill(SkillUser user)
    {
        ConcludeSkill(user);
    }

    public override void StepSkill(SkillUser user)
    {
        if(user.skillStep == 1){
            ApplyBuffs(user);
        }else if(user.skillStep > 1){
            ConcludeSkill(user);
        }
    }

    public void ApplyBuffs(SkillUser user){
        user.userStats.ApplyState(buffToApply);
        if(stillGraph){
            PoolObject p = user.userPool.SpawnTargetObject(stillGraph,1,user.transform);
            p.GetComponent<StillGraph>().Initialize(null, user.userStats.GetStack(buffToApply).stateDuration,user.transform);
        }
        if(castEffect){
            PoolObject p = user.userPool.SpawnTargetObject(castEffect,1,user.transform);
            p.transform.localPosition = Vector3.zero;
        }
        
    }

    public void ConcludeSkill(SkillUser user){
        user.userStats.canMove = true;
        user.usingSkill = false;
        user.userAnim.Play("Movement",0,0);
    }
}
