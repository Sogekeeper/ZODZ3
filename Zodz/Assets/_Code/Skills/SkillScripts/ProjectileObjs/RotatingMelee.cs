using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingMelee : ProjectileObject
{
    public float strengthMultiplier = 2;
    public DamageSource projectileDamageSource;
    public Collider2D hitCollider;
    public Animator anim;

    private GeneralDirection meleeDirection;

    public override void InitializeProjectile(SkillUser user)
    { 
        anim.Play("Idle",0,0);
        //hitCollider.enabled = false;
        meleeDirection = user.userAim.GetGeneralDirection();
        projectileDamageSource.damageValue = (int)(Random.Range(user.userStats.strength.Value-1,user.userStats.strength.Value+2) * strengthMultiplier);
        projectileDamageSource.hostileTo = user.userStats.enemyEntitySets;
        projectileDamageSource.owner = user.userStats;
        projectileDamageSource.skillType = SkillType.Melee;
        projectileDamageSource.damageType = DamageType.Physical;

        if(meleeDirection == GeneralDirection.DOWN){
            projectileDamageSource.transform.rotation = Quaternion.Euler(0,0,-90);
        }else if(meleeDirection == GeneralDirection.UP){
            projectileDamageSource.transform.rotation = Quaternion.Euler(0,0,90);
        }else if(meleeDirection == GeneralDirection.RIGHT){
            projectileDamageSource.transform.rotation = Quaternion.Euler(0,0,0);
        }else if(meleeDirection == GeneralDirection.LEFT){
            projectileDamageSource.transform.rotation = Quaternion.Euler(0,0,180);
        }

        StartCoroutine(HitRoutine());
    }

    public IEnumerator HitRoutine(){        
        hitCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        hitCollider.enabled = false;
        gameObject.SetActive(false);
    }

    public void HitCallback(){
        if(meleeDirection == GeneralDirection.DOWN){
            anim.Play("Down",0,0);
        }else if(meleeDirection == GeneralDirection.UP){
            anim.Play("Up",0,0);
        }else if(meleeDirection == GeneralDirection.RIGHT){
            anim.Play("Right",0,0);
        }else if(meleeDirection == GeneralDirection.LEFT){
            anim.Play("Left",0,0);
        }
    }
}
