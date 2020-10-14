using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_InstantAOE", menuName = "Skills/Instant AOE", order = 4)]
public class InstantAOE : Skill
{
    public DamageSource damageInformation;
    public float range = 10;
    public Multiplier rangeMultiplier;
    public EntityRuntimeSet globalEntitySet;
    [Header("Optional")]
    public RaceDependingAnimSet castingAnimSet;
    public float victimsKnockback = 0;
    public Multiplier knockbackMultiplier;
    public bool invertKnockback = false;


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
    //damageInformation.hostileTo = user.userStats.enemyEntitySets;
    user.ComputeSkill(this);
    user.userStats.ChangeMana(-skillCost);
    return true;
  }

  public override void InterruptSkill(SkillUser user)
  {
    EndSkill(user);
  }

  public override void StepSkill(SkillUser user)
  {
    if(user.skillStep == 1){
        ApplyAOE(user);
    }else if(user.skillStep > 1){
        EndSkill(user);
    }
  }

    public void ApplyAOE(SkillUser user){
        if(globalEntitySet.Items.Count > 0){
            for(int i = 0; i < globalEntitySet.Items.Count; i++){
                float dist = Vector2.Distance(user.transform.position, globalEntitySet.Items[i].transform.position);
                if(dist<range*rangeMultiplier.GetValue()){
                    globalEntitySet.Items[i].TakeDamage(damageInformation);
                    damageInformation.ApplyStatesToEntity(globalEntitySet.Items[i]);
                    globalEntitySet.Items[i].ApplyKnockback(user.transform.position,victimsKnockback*knockbackMultiplier.GetValue());
                }

            }
        }
    }

    public void EndSkill(SkillUser user){
        user.userStats.canMove = true;
        user.usingSkill = false;
        user.userAnim.Play("Movement",0,0);
    }
}
