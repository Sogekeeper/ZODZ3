using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionChecker : MonoBehaviour
{
    public Mission missionToCheck;
    public bool checkForActive;
    public bool checkForCompleted;
    public bool checkOnStart;
    public UnityEvent OnValidResult;
    public UnityEvent OnInvalidResult;

    private void Start() {
        if(checkOnStart) CheckMission();
    }

    public void CheckMission(){
        if(checkForActive && missionToCheck.isActive){
            OnValidResult?.Invoke(); return;
        }else if(checkForCompleted && missionToCheck.GetCompletedOutcome() != null){
            OnValidResult?.Invoke(); return;
        }
        OnInvalidResult?.Invoke();

    }
}
