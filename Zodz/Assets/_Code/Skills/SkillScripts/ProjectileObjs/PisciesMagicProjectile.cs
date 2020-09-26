using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisciesMagicProjectile : ProjectileObject
{
    public float travelSpeed = 10f;
    public float timeToFade = 0.8f;
    public float knockback = 1;
    public float userKnockback = 1;
    public float impactMultipler = 2f;
    public int flatTickDamage = 2;
    public float latchedTickRate = 0.8f;
    public int totalTicks = 6;
    public DamageSource projectileDamageSource;
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D col;

    private float fadeTimer;
    private float tickTimer;
    private int tickCounter;
    private Vector3 offsetFromVictim;
    private bool moving = true;
    private float currentSpeed;
    private EntityStats victim = null;

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
        }else{
            if(victim){
                if(victim.currentLife <= 0){
                    gameObject.SetActive(false);
                    victim=null;
                    return;
                }
                rb.MovePosition(victim.transform.position+offsetFromVictim);
                if(tickTimer > 0){
                    tickTimer -= Time.deltaTime;
                    if(tickTimer <= 0){
                        TickDamage();
                    }
                }                
            }
        }        
    }

    private void TickDamage(){
        tickTimer = latchedTickRate;
        projectileDamageSource.damageValue = flatTickDamage;
        projectileDamageSource.knockbackForce = 0;
        victim.TakeDamage(projectileDamageSource);
        tickCounter++;
        if(tickCounter >= totalTicks){
            victim=null;
            gameObject.SetActive(false);
        }
    }

    public override void InitializeProjectile(SkillUser user)
    {
        anim.ResetTrigger("fade");
        anim.Play("Travel",0,0);
        currentSpeed = travelSpeed;
        tickCounter = 0; fadeTimer =0; tickTimer = 0;
        col.enabled = true;
        moving = true;
        victim = null;
        fadeTimer = timeToFade;
        projectileDamageSource.damageValue = (int)(Random.Range(user.userStats.strength.Value-1,user.userStats.strength.Value+1) * impactMultipler);
        projectileDamageSource.hostileTo = user.userStats.enemyEntitySets;
        projectileDamageSource.knockbackForce = knockback;
        projectileDamageSource.owner = user.userStats;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EntityStats entityStats = other.GetComponent<EntityStats>();        
        if((entityStats && EntityRuntimeSet.DetectArrayOverlap(projectileDamageSource.hostileTo,entityStats.myEntitySets))){
            //gameObject.SetActive(false);
            victim = entityStats;
            tickTimer = latchedTickRate;
            offsetFromVictim = transform.position - victim.transform.position;
            anim.Play("Locked",0,0);
            moving = false;
            col.enabled = false;
        }else if(!entityStats){
            gameObject.SetActive(false);
            moving = false;
            col.enabled = false;
        }
    }
}
