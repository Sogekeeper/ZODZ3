using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public Skill rangedSkill;
    public TransformRuntimeSet rangedReferences;
    public float rangeToFight = 15f;
    public float rangeToMove = 10;
    public float timeToAttackAgain = 3f;
    public float timeToMoveAgain = 4f;
    public int minShots = 1; public int maxShots = 3;

    protected int shotsFired = 0;
    protected Transform previousRangedReference;

    [Header("Components")]
    public Aim aimToTarget;
    public Animator anim;
    public SkillUser skillUser;

    protected AIChaseBehaviour chase;
    protected Transform currentTarget;
    protected bool attacking = false;
    protected int layerMask;
    protected Vector3 initialPoint;
    protected float attackTimer;
    protected float moveTimer;

    protected void Start() {
        chase = GetComponent<AIChaseBehaviour>();
        layerMask = 1 << LayerMask.NameToLayer("Level");
        initialPoint = transform.position;
        currentTarget = AIAnalysis.GetClosestEnemy(skillUser.userStats.enemyEntitySets,transform,false,layerMask);
        moveTimer = timeToMoveAgain;
        attackTimer = timeToAttackAgain;
        StartCoroutine(CheckIfTargetWithinRange());
    }

    protected void Update() {        
        SetAimToTarget();
        if(!skillUser.usingSkill){
            SetMovAnimParams(); //isso não ta depois do if(currentTarget) pq eu quero deixar ele no idle caso sem target
        }

        if(!currentTarget) return;

        chase.enabled = skillUser.userStats.canMove; //skill will aways pause movement
        EnemyFSM();
    }

    public void EnemyFSM(){
        if(attackTimer > 0){
            attackTimer -= Time.deltaTime;
        }
        if(attacking){ // true if player within range
            if(moveTimer > 0){
                moveTimer -= Time.deltaTime;
                if(moveTimer <= 0 && (chase.target == null || chase.reachedEndOfPath)){
                    chase.target = GetNextRangedReference();
                    moveTimer = timeToMoveAgain;
                }
            }
            if(attackTimer <= 0 && !skillUser.usingSkill){
                skillUser.InitializeSkill(rangedSkill);
                shotsFired++;
                if(shotsFired >= maxShots || (shotsFired >= minShots && Random.Range(0,10) < 5)){
                    attackTimer = timeToAttackAgain;
                    shotsFired = 0;
                }
            }
        }
    }

    public Transform GetNextRangedReference(){
        int randomIndex = (int)Random.Range(0,rangedReferences.Items.Count);
        
        //first check if there's a new ref within range
        for(int i = 0; i < rangedReferences.Items.Count; i++){
            float curDist = Vector3.Distance(rangedReferences.Items[i].position,initialPoint);            
            if(!previousRangedReference || (previousRangedReference != rangedReferences.Items[randomIndex] && curDist <= rangeToMove)){
                previousRangedReference = rangedReferences.Items[randomIndex];
                return previousRangedReference;
            }
            else{
                randomIndex = (randomIndex + 1) % rangedReferences.Items.Count;
            }
        }
        //if it doesn't find anything, look for the next closest point;
        Transform result = null;
        for(int i = 0; i < rangedReferences.Items.Count; i++){
            float closest = Mathf.Infinity;
            float curDist = Vector3.Distance(rangedReferences.Items[i].position,transform.position);                        
            if(previousRangedReference != rangedReferences.Items[randomIndex] && curDist < closest){
                previousRangedReference = rangedReferences.Items[randomIndex];
                closest = curDist;
                result = previousRangedReference;
            }
            else{
                randomIndex = (randomIndex + 1) % rangedReferences.Items.Count;
            }
        }
        return result;
    }

    protected virtual void SetMovAnimParams(){
        if(chase.target && !chase.reachedEndOfPath){
            //anim.SetFloat("skillX",Aim.aimDirection.normalized.x);
            //anim.SetFloat("skillY",user.userAim.aimDirection.normalized.y);
            anim.SetFloat("horizontal",chase.currentMovDirection.normalized.x);
            anim.SetFloat("vertical",chase.currentMovDirection.normalized.y);
            anim.SetFloat("speed",1);
        }else{
            anim.SetFloat("speed",1);//mudei pra 1
        }
    }

    protected void SetAimToTarget(){
        if(!currentTarget) return;
        Vector2 dir = currentTarget.position - transform.position;
        aimToTarget.SetAimDirection(dir.normalized);
    }

    protected virtual IEnumerator CheckIfTargetWithinRange(){ 
        while(true){
            currentTarget = AIAnalysis.GetClosestEnemy(skillUser.userStats.enemyEntitySets,transform,false,layerMask);
            if(!skillUser.usingSkill && currentTarget){
                if(Vector2.Distance(transform.position, currentTarget.position) < rangeToFight){
                    attacking = true;
                }else{
                    attacking = false;
                }                       
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    
}
