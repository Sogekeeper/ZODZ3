using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirgoMagic : ProjectileObject
{
    [System.Serializable]
    public class PossibleState{
        public State targetState;
        public float chance;
    }

    public float travelSpeed = 10f;
    public float timeToFade = 0.8f;
    public float knockback = 1;
    public float mindMultiplier = 5f;
    public DamageSource projectileDamageSource;
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D col;

    public PossibleState[] possibleStates;

    private float fadeTimer;
    private bool moving = true;
    private float currentSpeed;
    
    private List<int> enemiesAlreadyHit;

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
        if(enemiesAlreadyHit == null)
            enemiesAlreadyHit = new List<int>();
        anim.ResetTrigger("fade");
        anim.Play("Appear",0,0);
        currentSpeed = 0;
        col.enabled = false;
        moving = false;
        fadeTimer = 0;
        enemiesAlreadyHit.Clear();
        RollStateToApply();
        projectileDamageSource.damageValue = (int)(Random.Range(user.userStats.mind.Value-1,user.userStats.mind.Value+2) * mindMultiplier);
        projectileDamageSource.hostileTo = user.userStats.enemyEntitySets;
        projectileDamageSource.owner = user.userStats;
        projectileDamageSource.knockbackForce = knockback;
    }

    public void StartToMove(){
        currentSpeed = travelSpeed;
        fadeTimer = timeToFade;
        moving = true;
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EntityStats entityStats = other.GetComponent<EntityStats>();        
        if((entityStats && EntityRuntimeSet.DetectArrayOverlap(projectileDamageSource.hostileTo,entityStats.myEntitySets) )){
            //gameObject.SetActive(false);
            enemiesAlreadyHit.Add(entityStats.gameObject.GetInstanceID());
            anim.Play("Impact",0,0);
            //moving = false;
            //col.enabled = false;
        }
    }

    private void RollStateToApply(){
        float chanceRoll = Random.Range(0,100);
        for (int i = 0; i < possibleStates.Length; i++)
        {
            if(possibleStates[i].chance >= chanceRoll){
                projectileDamageSource.statesToApply = new State[1];
                projectileDamageSource.statesToApply[0] = possibleStates[i].targetState;
                return;
            }
        }
    }
}
