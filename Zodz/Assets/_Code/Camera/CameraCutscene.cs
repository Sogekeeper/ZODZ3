using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraCutscene : MonoBehaviour
{
    public dg_simpleCamFollow targetCameraFollowScript;
    public float timeToReachTarget = 1f;
    public float timeLookingAtTarget = 3f;
    public bool endWithoutLerp = false;

    [Header("Optional Disable Player Actions")]
    public bool lookForStatsIfMissing = false;
    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    
    public UnityEvent OnReachCamTarget;
    public UnityEvent OnEndCutscene;

    private Transform originalTarget;
    private float timer = 0;
    private int currentTween = -1;

    private void Start() {
        if(targetCameraFollowScript == null) targetCameraFollowScript = FindObjectOfType<dg_simpleCamFollow>();
        originalTarget = targetCameraFollowScript.target;//just to be sure
    }

    public void StartCutscene(Transform target){
        if(lookForStatsIfMissing){
            playerStats = FindObjectOfType<PlayerStats>();
            playerMovement = FindObjectOfType<PlayerMovement>();
            playerCombat = FindObjectOfType<PlayerCombat>();
        }
        if(currentTween >= 0) LeanTween.cancel(targetCameraFollowScript.gameObject,currentTween);
        originalTarget = targetCameraFollowScript.target;
        targetCameraFollowScript.enabled = false;
        
        currentTween = targetCameraFollowScript.transform.LeanMove(new Vector3(target.position.x,target.position.y,targetCameraFollowScript.transform.position.z), 
            timeToReachTarget).setEaseOutSine().setOnComplete(ReachTargetCallback).uniqueId;

        if(playerStats){
            playerStats.CanMove(false);
            playerStats.CanAct(false);
        }
        if(playerMovement) playerMovement.canInput = false;
        if(playerCombat) playerCombat.canInput = false;


    }

    private void ReachTargetCallback(){
        timer = timeLookingAtTarget;
        OnReachCamTarget?.Invoke();
    }

    private void EndCutscene(){
        if(playerStats){
            playerStats.CanMove(true);
            playerStats.CanAct(true);
        }
        if(playerMovement) playerMovement.canInput = true;
        if(playerCombat) playerCombat.canInput = true;
        targetCameraFollowScript.enabled = true;
        currentTween = -1;
        OnEndCutscene?.Invoke();
    }

    private void Update() {
        if(timer > 0){
            timer -= Time.deltaTime;
            if(timer <= 0){
                ManualEndCutscene();
            }
        }
    }

    public void ManualEndCutscene(){
        if(endWithoutLerp) EndCutscene();
        else targetCameraFollowScript.transform.LeanMove(new Vector3(originalTarget.position.x,originalTarget.position.y,
            targetCameraFollowScript.transform.position.z),timeToReachTarget).setEaseOutSine().setOnComplete(EndCutscene);
    }
}
