using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillGraph : MonoBehaviour
{
    public Animator anim;

    private float timer;
    private Transform attached;
    private State.StateStack trackingStack;

    private void Update() {
        if(attached){
            transform.position = attached.position;
        }
        if(timer > 0){
            timer -= Time.deltaTime;
            if(timer <= 0){
                gameObject.SetActive(false);
            }
        }
    }

    public void Initialize(Transform target, float duration, Transform parent = null){
        attached = target;
        timer = duration;
        if(parent){
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }
        
        anim.Play("Active",0,0);
    }
}
