using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SagittariusArrowRain : ProjectileObject
{
    public float baseHitDamage = 4;
    public Animator anim;
    public DamageSource damageInfo;

    public override void InitializeProjectile(SkillUser user)
    {
        anim.Play("Fire",0,0);
        damageInfo.damageValue = (int)baseHitDamage;
        damageInfo.hostileTo = user.userStats.enemyEntitySets;
        damageInfo.owner = user.userStats;
        damageInfo.knockbackForce = 0;

        //skill e damage type setada no inspector
    }    

}
