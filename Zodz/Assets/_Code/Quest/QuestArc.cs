using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestArc", menuName = "Questing/Quest Arc", order = 0)]
public class QuestArc : ScriptableObject
{
    public string questName;
    public Mission starterMission;
    public GameEvent questUpdateEvent;//para interface
    public bool completed = false;
    public bool rewarded = false;
    public Reward reward;
    public List<Mission> missions = new List<Mission>();

    public bool initialized = false;

    [TextArea]public string questDescription;

    public void InitializeQuest(){
        if(initialized) return;
        missions.Clear();
        missions.Add(starterMission);
        missions[0].isActive = true;
        completed = false;
        initialized = true;        
    }

    public Mission GetCurrentMission(){
        if(!initialized || missions.Count <= 0) return null;
        for(int i = missions.Count - 1; i >= 0;i--){
            if(missions[i].isActive) return missions[i];
        }
        return null;
    }

    public void NextMission(Mission mission){
        DeactivateMissions();
        mission.isActive = true;
        missions.Add(mission);
        questUpdateEvent.Raise();        
    }

    public void CompleteQuest(){
        DeactivateMissions();
        completed = true;
        questUpdateEvent.Raise();    
    }

    public void Reward(PoolContainer pooler, Vector3 spawnPosition){
        PoolObject po = pooler.SpawnTargetObject(reward.thisPoolObject,2);
        po.transform.position = spawnPosition;
        rewarded = true;
        questUpdateEvent.Raise();
    }

    private void DeactivateMissions(){
        if(missions == null || missions.Count <= 0) return;
        for(int i = 0; i < missions.Count; i++){
            missions[i].isActive = false;
        }
    }

    [ContextMenu("DEBUG - Reset Quest")]
    public void ResetQuestArc(){
        rewarded = false;
        completed = false;
        initialized = false;
        for (int i = 0; i < missions.Count; i++)
        {
            missions[i].ResetMission();
        }
        missions.Clear();
    }
}
