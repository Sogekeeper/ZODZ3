using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public Skill rangedSkill;
    public TransformRuntimeSet rangedReferences;
    public float rangeToFight = 15f;
    public float rangeToMove = 10;
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

    protected void Start() {
        chase = GetComponent<AIChaseBehaviour>();
        layerMask = 1 << LayerMask.NameToLayer("Level");
        initialPoint = transform.position;
        currentTarget = AIAnalysis.GetClosestEnemy(skillUser.userStats.enemyEntitySets,transform,false,layerMask);
        StartCoroutine(CheckIfTargetWithinRange());
    }

    protected void Update() {
        SetAimToTarget();
        if(!skillUser.usingSkill){
            SetMovAnimParams(); //isso não ta depois do if(currentTarget) pq eu quero deixar ele no idle caso sem target
        }

        if(!currentTarget) return;

        chase.enabled = skillUser.userStats.canMove;
        EnemyFSM();
    }

    public void EnemyFSM(){
        if(attacking){
            if(chase.target && chase.reachedEndOfPath && !skillUser.usingSkill){
                skillUser.InitializeSkill(rangedSkill);
                shotsFired++;
                if(shotsFired >= maxShots || (shotsFired >= minShots && Random.Range(0,10) < 5)){
                    chase.target = GetNextRangedReference();      
                }
            }
        }
    }

    public Transform GetNextRangedReference(){
        int randomIndex = (int)Random.Range(0,rangedReferences.Items.Count);
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
        return null;
    }

    protected void SetMovAnimParams(){
        if(chase.target && !chase.reachedEndOfPath){
            //anim.SetFloat("skillX",Aim.aimDirection.normalized.x);
            //anim.SetFloat("skillY",user.userAim.aimDirection.normalized.y);
            anim.SetFloat("horizontal",chase.currentMovDirection.normalized.x);
            anim.SetFloat("vertical",chase.currentMovDirection.normalized.y);
            anim.SetFloat("speed",1);
        }else{
            anim.SetFloat("speed",0);
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
            if(!skillUser.usingSkill){
                if(Vector2.Distance(transform.position, currentTarget.position) < rangeToFight){
                    if(chase.target == null){
                        chase.target = GetNextRangedReference();
                    }
                    attacking = true;
                }else{
                    attacking = false;
                    chase.target = null;
                }                       
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    
}
