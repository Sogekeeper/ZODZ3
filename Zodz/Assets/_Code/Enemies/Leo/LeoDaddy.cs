using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoDaddy : EnemyMeleeSimple
{
    [Header("Leo Daddy Stuff")]
    public Skill rangedSkill;
    public Transform[] rangedReferences;
    public int maxMeleeAttempts = 2;

    private int meleeAttempts = 0;
    private bool goingRanged = false;
    private Transform previousRangedReference;

    public Transform GetNextRangedReference(){
        int randomIndex = Random.Range(0,rangedReferences.Length-1);
        float distToRef = Mathf.Infinity;
        for(int i = 0; i < rangedReferences.Length; i++){
            float currDist = Vector3.Distance(transform.position,rangedReferences[randomIndex].position);
            if(!previousRangedReference || previousRangedReference != rangedReferences[randomIndex]){
                if(currDist < distToRef){
                    previousRangedReference = rangedReferences[randomIndex];
                    distToRef = currDist;
                    return previousRangedReference;                    
                }else{
                    randomIndex = (randomIndex + 1) % rangedReferences.Length;    
                }
            }
            else{
                randomIndex = (randomIndex + 1) % rangedReferences.Length;
            }
        }
        return null;
    }

    protected override void EnemyFSM(){
        if(goingRanged){
            if(chase.target){
                if(chase.reachedEndOfPath){
                    Attack();
                    goingRanged = false;
                    meleeAttempts = 0;
                }
            }
        }else{
            float distToTarget = Vector2.Distance(transform.position, currentTarget.position);
            if(distToTarget < rangeToAttack && !skillUser.usingSkill){
                Attack();
                meleeAttempts++;
                if(meleeAttempts >= maxMeleeAttempts || Random.Range(0,100) < 50){
                    goingRanged = true;
                    chase.target = GetNextRangedReference();
                }
            }
        }
    }

    protected override void Attack(){
        chase.target = null;
        if(goingRanged){
            skillUser.InitializeSkill(rangedSkill);
        }else{
            skillUser.InitializeSkill(attackSkill);            
        }
    }

    protected override IEnumerator CheckIfTargetWithinRange(){ 
        while(true){
            currentTarget = AIAnalysis.GetClosestEnemy(skillUser.userStats.enemyEntitySets,transform,false,layerMask);
            if(!skillUser.usingSkill && !goingRanged){
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
}
