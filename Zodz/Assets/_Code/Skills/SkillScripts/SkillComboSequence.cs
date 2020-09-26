using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Combo", menuName = "Skills/Combo Sequence", order = 3)]
public class SkillComboSequence : Skill
{
    public float timeToUseNextSkill = 1.0f;
    public Skill[] skillsToUse;

  public override void FrameUpdate(SkillUser user)
  {
    //not necessary
  }

  public override bool Initialize(SkillUser user)
  {
    if(skillsToUse == null || skillsToUse.Length <= 0){//não tem skills configuradas
        Debug.LogError("Skill Combo Sequence Object has no skills.");
        return false;
    }
    //tem skills
    Skill nextSkill = skillsToUse[0];
    if(user.timeSinceLastSkill <= timeToUseNextSkill){//vamos ver qual é a proxima skill a se usar
        int targetSkillIndex = 0;
        for(int i = 0; i < skillsToUse.Length; i++){
            if(user.lastUsedSkill != null && user.lastUsedSkill == skillsToUse[i]){
                targetSkillIndex = (i + 1) % skillsToUse.Length;
                //Debug.Log("advancing skill");
            }
        }
        //Debug.Log("Combo skill id: "+targetSkillIndex);
        nextSkill = skillsToUse[targetSkillIndex];
    }
    user.InitializeSkill(nextSkill);
    return false;
  }

  public override void InterruptSkill(SkillUser user)
  {
    //not necessary
  }

  public override void StepSkill(SkillUser user)
  {
    //not necessary
  }
}
