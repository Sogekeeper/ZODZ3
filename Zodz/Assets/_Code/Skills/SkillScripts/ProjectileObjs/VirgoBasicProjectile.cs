using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirgoBasicProjectile : ProjectileObject
{
    public float travelSpeed = 10f;
    public float timeToFade = 0.8f;
    public float knockback = 1;
    public DamageSource projectileDamageSource;
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D col;

    private float fadeTimer;
    private bool moving = true;
    private float currentSpeed;
    private int levelLayer;

    private void Start() {
        levelLayer = LayerMask.NameToLayer("Level");
    }
    
    private void Update() {
        if(moving){
            rb.MovePosition(rb.position + (Vector2)transform.up * currentSpeed * Time.fixedDeltaTime);
            if(fadeTimer > 0){
                fadeTimer -= Time.deltaTime;
                if(fadeTimer <= 0){
                    col.enabled = false;
                    currentSpeed = LeanTween.easeOutBack(currentSpeed,currentSpeed/5,0.2f);
                    anim.SetTrigger("fade");
                }
            }
        }
        
    }

    public override void InitializeProjectile(SkillUser user)
    {
        anim.ResetTrigger("fade");
        anim.Play("Travel",0,0);
        currentSpeed = travelSpeed;
        //col.enabled = true;
        moving = true;
        fadeTimer = timeToFade;
        projectileDamageSource.owner = user.userStats;
        projectileDamageSource.damageValue = (int)(Random.Range(user.userStats.mind.Value-1,user.userStats.mind.Value+1) * 1.5f);
        projectileDamageSource.hostileTo = user.userStats.enemyEntitySets;
        projectileDamageSource.knockbackForce = knockback;
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EntityStats entityStats = other.GetComponent<EntityStats>();        
        if((entityStats && EntityRuntimeSet.DetectArrayOverlap(projectileDamageSource.hostileTo,entityStats.myEntitySets)) || (other.gameObject.layer == levelLayer)){
            //gameObject.SetActive(false);
            anim.Play("Impact",0,0);
            moving = false;
            col.enabled = false;
        }
    }
}
