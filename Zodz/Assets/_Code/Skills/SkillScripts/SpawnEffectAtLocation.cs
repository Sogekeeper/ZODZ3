using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_SpawnEffectAt", menuName = "Skills/Spawn At Focus", order = 4)]
public class SpawnEffectAtLocation : Skill
{
  public float range = 2;
  public PoolObject effectToSpawn;
  public RaceDependingAnimSet castingAnimSet;

  public override void FrameUpdate(SkillUser user)
  {
    
  }

  public override bool Initialize(SkillUser user)
  {
    if(castingAnimSet)
        user.ReplaceSkillAnimationSet(castingAnimSet.GetSetForRace(user.userStats.baseRace));
    SetUserAnimParams(user);
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
        CastSkill(effectToSpawn, user);
      }else if(user.skillStep > 1){
        ConcludeSkill(user);
    }
  }

  public void CastSkill(PoolObject projectile, SkillUser user){
    PoolObject p = user.userPool.SpawnTargetObject(projectile,1);
    if(user.userAim.focusPoint != null)p.transform.position = user.userAim.focusPoint;
    p.transform.position = new Vector3(p.transform.position.x,p.transform.position.y,0);
    p.GetComponent<ProjectileObject>().InitializeProjectile(user);
  }

  private void ConcludeSkill(SkillUser user){
    user.userStats.canMove = true;
    user.usingSkill = false;
    user.userAnim.Play("Movement",0,0);
  }

  private void SetUserAnimParams(SkillUser user){
    user.userAnim.SetFloat("skillX",Mathf.Round(user.userAim.aimDirection.normalized.x));
    user.userAnim.SetFloat("skillY",Mathf.Round(user.userAim.aimDirection.normalized.y));
    user.userAnim.SetFloat("horizontal",user.userAim.aimDirection.normalized.x);
    user.userAnim.SetFloat("vertical",user.userAim.aimDirection.normalized.y);
  }
}
