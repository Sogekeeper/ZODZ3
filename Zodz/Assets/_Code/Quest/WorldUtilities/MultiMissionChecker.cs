using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiMissionChecker : MonoBehaviour
{
    [System.Serializable]
    public class MissionCheck{
        public Mission targetMission;
        public UnityEvent OnValid;
    }
    
    public bool checkOnStart = false;
    public MissionCheck[] missionChecks;
    public UnityEvent OnAllInvalid;

    [Header("Debug")]
    public int forceValidIndex = -1;

    private void Start() {
        if(checkOnStart){
            CheckAllMissions();
        }
    }

    public void CheckAllMissions(){
        if(missionChecks == null || missionChecks.Length <= 0)return;

        for (int i = 0; i < missionChecks.Length; i++)
        {
            if(missionChecks[i].targetMission.isActive || i == forceValidIndex){
                missionChecks[i].OnValid?.Invoke();
                return;
            }
        }

        OnAllInvalid?.Invoke();
    }
}
