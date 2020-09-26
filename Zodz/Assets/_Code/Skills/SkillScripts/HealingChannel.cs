using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_HealingChannel", menuName = "Skills/Healing Channel", order = 4)]
public class HealingChannel : Skill
{
    public float mindMultiplier = 5;
    public float healingTime = 3f;
    public RaceDependingAnimSet activateAnim;
    public RaceDependingAnimSet healingAnim;
    public RaceDependingAnimSet stopAnim;

  public override void FrameUpdate(SkillUser user)
  {
    if(user.timeSinceLastSkill >= healingTime){
        if(stopAnim)
            user.ReplaceSkillAnimationSet(stopAnim.GetSetForRace(user.userStats.baseRace));
        //user.userAnim.Play("Skill",0,0);
    }
  }

  public override bool Initialize(SkillUser user)
  {
    if(activateAnim)
        user.ReplaceSkillAnimationSet(activateAnim.GetSetForRace(user.userStats.baseRace));    
    user.userStats.rb.velocity = Vector3.zero;
    user.userStats.canMove = false;
    user.userAnim.Play("Skill",0,0);
    user.ComputeSkill(this);
    user.userStats.ChangeMana(-skillCost);
    return true;
  }

  public override void InterruptSkill(SkillUser user)
  {
        user.userStats.canMove = true;
        user.usingSkill = false;
  }

  public override void StepSkill(SkillUser user)
  {
    if(user.timeSinceLastSkill >= healingTime){
        user.userStats.canMove = true;
        user.usingSkill = false;
        user.userAnim.Play("Movement",0,0);
        return;
    }
    if(user.skillStep == 1){
        if(healingAnim)
            user.ReplaceSkillAnimationSet(healingAnim.GetSetForRace(user.userStats.baseRace));
        user.userAnim.Play("Skill",0,0);
    }
    if(user.skillStep > 1){
        Heal(user);
    }
  }

  private void Heal(SkillUser user)
  {
    user.userStats.Heal((int)(user.userStats.mind.Value * mindMultiplier));    
  }
}
