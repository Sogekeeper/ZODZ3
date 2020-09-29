using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionChecker : MonoBehaviour
{
    public Mission missionToCheck;
    public bool checkForActive;
    public bool checkForCompleted;
    public UnityEvent OnValidResult;

    public void CheckMission(){
        if(checkForActive && missionToCheck.isActive){
            OnValidResult?.Invoke();
        }else if(checkForCompleted && missionToCheck.GetCompletedOutcome() != null){
            OnValidResult?.Invoke();
        }

    }
}
