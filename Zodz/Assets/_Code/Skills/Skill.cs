using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
  public string skillName;
  [TextArea]public string skillShortDescription;
  [TextArea]public string skillLongDescription;
  public Sprite skillIcon;
  public int skillCost;
  //public bool isMelee = false;
  public float cooldown = 0;

  public abstract bool Initialize(SkillUser user); //ao usar skill, retorna 

  public abstract void StepSkill(SkillUser user);//animação da skill chama nos pontos que precisar
  
  public abstract void FrameUpdate(SkillUser user); //skill user chamada todo frame durante skill
  
  public abstract void InterruptSkill(SkillUser user); //levou ou parou de segurar skill

}
public enum SkillType{
  None,
  Melee,
  Ranged
};
public enum DamageType{
  None,
  Physical,
  Magical
};