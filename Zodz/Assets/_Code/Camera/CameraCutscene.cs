using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutscene : MonoBehaviour
{
    public dg_simpleCamFollow targetCameraFollowScript;
    public float lookDuration;

    private Transform originalTarget;
    private float timer = 0;

    private void Start() {
        originalTarget = targetCameraFollowScript.target;//just to be sure
    }

    public void StartCutscene(Transform target){
        timer = lookDuration;
        originalTarget = targetCameraFollowScript.target;
        targetCameraFollowScript.target = target;
    }

    private void Update() {
        if(timer > 0){
            timer -= Time.deltaTime;
            if(timer <= 0){
                targetCameraFollowScript.target = originalTarget;
            }
        }
    }
}
