using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AIChaseBehaviour))]
public class SimpleFollow : MonoBehaviour
{
    public float desiredDistance = 2;
    public Animator anim;
    public UnityEvent[] OnReachTarget;

    private AIChaseBehaviour chase;
    private bool triggered = false;
    private int currentMission = 0;

    private void Awake() {
        chase = GetComponent<AIChaseBehaviour>();
    }

    private void Update() {
        if(chase.target && chase.GetDistanceToTarget() < desiredDistance && !triggered){
            chase.target = null;
            triggered = true;
            if(OnReachTarget != null && OnReachTarget.Length > 0){
                OnReachTarget[currentMission]?.Invoke();
                currentMission = (currentMission + 1) % OnReachTarget.Length;
            }
        }
    }

    public void SetTarget(Transform target){
        chase = GetComponent<AIChaseBehaviour>();
        chase.target = target;
        triggered = false;
    }

    private void SetMovAnimParams(){
        if(!anim) return;
        if(chase.target && !chase.reachedEndOfPath){            
            anim.SetFloat("horizontal",chase.currentMovDirection.normalized.x);
            anim.SetFloat("vertical",chase.currentMovDirection.normalized.y);
            anim.SetFloat("speed",1);
        }else{
            anim.SetFloat("speed",0);
        }
    }
}
