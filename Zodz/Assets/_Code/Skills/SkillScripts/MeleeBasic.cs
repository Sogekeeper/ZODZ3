using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_MeleeBasic", menuName = "Skills/Melee Basic", order = 2)]
public class MeleeBasic : Skill
{ 
    public PoolObject meleeHitPrefab;
    public float baseKnockbackForce = 10f;
    public float forwardImpulseForce = 10f;
    public bool keepUpdatingSkillAnim = false; //inimigos

    [Header("Animation Clips")]
    public RaceDependingAnimSet attackAnimSet;

    public override void FrameUpdate(SkillUser user)
    {
        if(keepUpdatingSkillAnim){
            user.userAnim.SetFloat("skillX",Mathf.Round(user.userAim.aimDirection.normalized.x));
            user.userAnim.SetFloat("skillY",Mathf.Round(user.userAim.aimDirection.normalized.y));
        }
    }

    public override bool Initialize(SkillUser user)
    {
        user.userStats.rb.velocity = Vector3.zero;
        if(attackAnimSet)
            user.ReplaceSkillAnimationSet(attackAnimSet.GetSetForRace(user.userStats.baseRace));
        //user.skillStep= 0;
        
        user.userStats.canMove = false;
        user.userAnim.Play("Skill",0,0);
        user.userAnim.SetFloat("skillX",Mathf.Round(user.userAim.aimDirection.normalized.x));
        user.userAnim.SetFloat("skillY",Mathf.Round(user.userAim.aimDirection.normalized.y));
        user.userAnim.SetFloat("horizontal",Mathf.Round(user.userAim.aimDirection.normalized.x));
        user.userAnim.SetFloat("vertical",Mathf.Round(user.userAim.aimDirection.normalized.y));

        user.userStats.ChangeMana(-skillCost);
        user.ComputeSkill(this);
        return true;
    }

    public override void InterruptSkill(SkillUser user)
    {
        ConcludeSlash(user);
    }

    public override void StepSkill(SkillUser user)
    {
        //Debug.Log("step");
        if(user.skillStep == 1){
            SpawnSlash(user);
        }else if(user.skillStep >1){
            ConcludeSlash(user);
        }
    }

    private void SpawnSlash(SkillUser user){
        PoolObject slash = user.userPool.SpawnTargetObject(meleeHitPrefab, 2);
        slash.transform.position = user.transform.position;
        DamageSource dmg = slash.GetComponent<DamageSource>();
        dmg.damageValue = (int)user.userStats.strength.Value;
        dmg.hostileTo = user.userStats.enemyEntitySets;
        dmg.knockbackForce = baseKnockbackForce;
        dmg.skillType = SkillType.Melee;
        dmg.damageType = DamageType.Physical;
        //slash.transform.rotation = user.userAim.optionalRotatingPointer.rotation; //temporario, não ideal, precisamos de um método pra calcular rotação
        user.userAim.RotateObjectToAim(slash.transform);

        
        dmg.owner = user.userStats;
        dmg.manaReplenishAmount = 2;
        
    }

    private void ConcludeSlash(SkillUser user){
        user.userStats.canMove = true;
        user.usingSkill = false;
        user.userAnim.Play("Movement",0,0);
    }
}
