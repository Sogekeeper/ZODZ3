using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIChaseBehaviour))]
public class EnemyMeleeSimple : MonoBehaviour
{
    //ta uma bagunça esse código, mas to rushando pro J1
    //no momento ele tira o target do AIChaseBehaviour toda vez que precisar parar o movimento
    public float rangeToChase = 20f;
    public float rangeToStopChase = 20f;
    public float rangeToAttack = 1.5f;

    public Skill attackSkill;
    public Aim aimToTarget;
    public Animator anim;
    public SkillUser skillUser;

    protected AIChaseBehaviour chase;
    protected Transform currentTarget;
    protected bool chasing = false;

    protected int layerMask = 0;

    protected void Start() {
        chase = GetComponent<AIChaseBehaviour>();
        layerMask = 1 << LayerMask.NameToLayer("Level");
        currentTarget = AIAnalysis.GetClosestEnemy(skillUser.userStats.enemyEntitySets,transform, skillUser.userStats, false,layerMask);
        StartCoroutine(CheckIfTargetWithinRange());        
    }

    protected void OnEnable() {
        //StopCoroutine(CheckIfTargetWithinRange());
        //StartCoroutine(CheckIfTargetWithinRange());
    }

    protected virtual IEnumerator CheckIfTargetWithinRange(){ 
        while(true){
            currentTarget = AIAnalysis.GetClosestEnemy(skillUser.userStats.enemyEntitySets,transform, skillUser.userStats,false,layerMask);
            if(!skillUser.usingSkill){
                if(currentTarget 
                    && Vector2.Distance(transform.position, currentTarget.position) < rangeToChase
                        && !chasing){
                    chase.target = currentTarget;
                    chasing = true;
                }else if(currentTarget && chasing
                            && Vector2.Distance(transform.position, currentTarget.position) < rangeToStopChase){
                    chase.target = currentTarget;
                }else{
                    chasing = false;
                    currentTarget = null;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected void Update() {
        SetAimToTarget();
        if(!skillUser.usingSkill){
            SetMovAnimParams(); //deixar no idle caso nada acontecendo ou movimentar caso target
        }

        if(!currentTarget) return;

        chase.enabled = skillUser.userStats.canMove;
        EnemyFSM();
    }
    
    protected virtual void EnemyFSM(){              
        float distToTarget = Vector2.Distance(transform.position, currentTarget.position);
        if(distToTarget < rangeToAttack && !skillUser.usingSkill){
            Attack();
        }
    }

    protected virtual void Attack(){
        skillUser.InitializeSkill(attackSkill);
        chase.target = null;
    }

    protected void SetMovAnimParams(){
        if(chase.target && !chase.reachedEndOfPath){
            //anim.SetFloat("skillX",Aim.aimDirection.normalized.x);
            //anim.SetFloat("skillY",user.userAim.aimDirection.normalized.y);
            anim.SetFloat("horizontal",Mathf.Round(chase.currentMovDirection.normalized.x));
            anim.SetFloat("vertical",Mathf.Round(chase.currentMovDirection.normalized.y));
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

   


}
