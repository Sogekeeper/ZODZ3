using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleIceShard : ProjectileObject
{
    public float travelSpeed = 10f;
    public DamageSource projectileDamageSource;
    public Rigidbody2D rb;

    private void Update() {
        rb.MovePosition(rb.position + (Vector2)transform.up * travelSpeed * Time.fixedDeltaTime);
    }

    public override void InitializeProjectile(SkillUser user)
    {
        projectileDamageSource.damageValue = (int)Random.Range(user.userStats.spirit.Value-1,user.userStats.spirit.Value+1) * 2;
        projectileDamageSource.hostileTo = user.userStats.enemyEntitySets;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EntityStats entityStats = other.GetComponent<EntityStats>();
        if(entityStats && EntityRuntimeSet.DetectArrayOverlap(projectileDamageSource.hostileTo,entityStats.myEntitySets)){
            gameObject.SetActive(false);
        }else if (!entityStats){
            gameObject.SetActive(false);
        }
    }
}
