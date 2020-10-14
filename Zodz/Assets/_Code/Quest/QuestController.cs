using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestController : ScriptableObject
{
    //OLD NÃO UTILIZADO

    public Quest activeQuest;
    
    public void ChangeActiveQuest(Quest target){
        activeQuest = target;
    }
    public void ClearActiveQuest(){
        activeQuest = null;
    }

    public Quest.QuestInfo GetCurrentInfo(){
        if(!activeQuest) return null;
        return activeQuest.quests[activeQuest.completedQuests];
    }
}