using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectiveSetter : MonoBehaviour
{
    //OLD - PARA MUDAR

    public Quest targetQuestArc;
    public int questId;
    
    private void OnEnable() {
        if(targetQuestArc && targetQuestArc.quests.Length < questId){
            targetQuestArc.quests[questId].currentObjective = transform;
        }
    }

    private void OnDisable() {
        if(targetQuestArc && targetQuestArc.quests.Length < questId){
            targetQuestArc.quests[questId].currentObjective = null;
        }        
    }
}
