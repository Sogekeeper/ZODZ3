using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

[CreateAssetMenu(fileName = "Skill_ProjectileVolley", menuName = "Skills/Projectile Volley", order = 3)]
public class ProjectileVolley : Skill
{
    public PoolObject baseProjectile;
    public RaceDependingAnimSet castingAnimSet;
    public int numberOfProjectiles;
  
    [Header("Optional")]
    public float castingKnockback = 0;
    public bool forwardCastingKnockback = false;
    public float spawnDistanceMultiplier = 1;
    public PoolObject ultimateProjectile;
    public bool updateAimDuringVolley = true;
    public bool rotateBasicProjectile = true;
    public bool rotateUltimateProjectile = true;
    public AudioClip soundOnCastEach;

  public override void FrameUpdate(SkillUser user)
  {
    if(updateAimDuringVolley) SetUserAnimParams(user);
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
    ConcludeVolley(user);
  }

  public override void StepSkill(SkillUser user)
  {
    if(user.skillStep > 0 && user.skillStep <= numberOfProjectiles){
      if(user.skillStep == numberOfProjectiles && ultimateProjectile){
        CastProjectile(ultimateProjectile, user, rotateUltimateProjectile);
      }else{
        CastProjectile(baseProjectile, user, rotateBasicProjectile);
      }
    }else if(user.skillStep > numberOfProjectiles){
      ConcludeVolley(user);
    }
  }

  private void CastProjectile(PoolObject projectile, SkillUser user, bool rotateProjectile = true){
    PoolObject p = user.userPool.SpawnTargetObject(projectile,10);
    if(soundOnCastEach)EazySoundManager.PlaySound(soundOnCastEach,0.1f);
    if(user.skillSpawnPoint)
      p.transform.position = user.skillSpawnPoint.position;
    else
      p.transform.position = user.transform.position;
    //p.transform.position += user.userAim.aimDirection.normalized * spawnDistanceMultiplier;    
    if(rotateProjectile) user.userAim.RotateObjectToAim(p.transform);
    p.GetComponent<ProjectileObject>().InitializeProjectile(user);
    if(castingKnockback > 0){
      user.userStats.rb.velocity = Vector3.zero;
      Vector3 kDirection = user.userAim.aimDirection.normalized;
      if(!forwardCastingKnockback){
        kDirection.y *= -1; kDirection.x *= -1; //eu sei que dava pra fazer numa conta só, mas só assim vai
      }
      user.userStats.rb.AddForce(kDirection * castingKnockback);
    }
  }

  private void ConcludeVolley(SkillUser user){
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
