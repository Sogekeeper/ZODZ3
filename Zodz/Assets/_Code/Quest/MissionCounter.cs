using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestMissionCounter", menuName = "Questing/Quest Mission Counter", order = 2)]
public class MissionCounter : ScriptableObject
{
    public Mission parentMission;
    public int targetAmount;
    public int currentAmount;
    [Header("Other Settings")]
    public bool goAboveTarget = false;
    public bool ignoreMissionActiveCondition = false;
    public GameEvent questUpdateEvent;

    public void IncreaseCounter(int amount){
        if(!parentMission.isActive && !ignoreMissionActiveCondition) return;

        currentAmount += amount;
        if(currentAmount >= targetAmount){
            if(!goAboveTarget) currentAmount = targetAmount;
            parentMission?.ResolveCompletedCounter(this);
        }

        questUpdateEvent?.Raise();
    }

}
