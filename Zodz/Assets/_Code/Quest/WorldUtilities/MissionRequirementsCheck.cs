using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionRequirementsCheck : MonoBehaviour
{
    //each requirement can be checked for: is active, is completed, either of those, is not active neither completed.
    [System.Serializable]
    public class Requirement{
        public Mission targetMission;
        public bool shouldBeActive = false;
        public bool shouldBeCompleted = false;
    }
    public bool checkOnStart;
    public Requirement[] requirements;
    public UnityEvent OnValid;
    public UnityEvent OnInvalid;

    private void Start() {
        if(checkOnStart){
            CheckMissions();
        }
    }

    public bool CheckMissions(){
        if(requirements == null || requirements.Length <= 0) return false;
        for (int i = 0; i < requirements.Length; i++)
        {
            if(requirements[i].shouldBeActive && !requirements[i].targetMission.isActive){
                OnInvalid?.Invoke();
                return false;
            }
            else if(requirements[i].shouldBeCompleted && requirements[i].targetMission.GetCompletedOutcome() == null){
                OnInvalid?.Invoke();
                return false;
            }else if(!requirements[i].shouldBeActive && requirements[i].targetMission.isActive){
                OnInvalid?.Invoke();
                return false;
            }
            else if(!requirements[i].shouldBeCompleted && requirements[i].targetMission.GetCompletedOutcome() != null){
                OnInvalid?.Invoke();
                return false;
            }
        }
        OnValid?.Invoke();
        return true;
    }
}
