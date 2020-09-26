using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestDependingEvent : MonoBehaviour
{
    public Quest targetQuest;
    public int targetMissionIndex;

    public UnityEvent NotCompletedResult;
    public UnityEvent CompletedResult;

    public void TriggerResult(){
        if(!targetQuest || targetQuest.completedQuests < targetMissionIndex){
            NotCompletedResult?.Invoke();
        }else{
            CompletedResult?.Invoke();
        }
    }
}
