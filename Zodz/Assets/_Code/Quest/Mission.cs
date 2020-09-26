using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestMission", menuName = "Questing/Quest Mission", order = 1)]
public class Mission : ScriptableObject
{
    public QuestArc parentQuestArc;
    public bool isActive = false;

    [System.Serializable]
    public class Goal{ //cada objetivo separado
        [TextArea]
        public string shortDescription;
        public bool completed;
        public MissionCounter goalCounter;
        public bool showCounter;
    }
    [System.Serializable]
    public class Outcome{//resultados possíveis, pode ter mais de um goal
        public bool completed;
        public Mission nextMission;
        public Goal[] goals;

        public bool CheckGoals(){
            if(completed) return true; //caso outcome seja completada manualmente sem Counters
            if(goals == null || goals.Length <= 0) return false; //impedindo bugs

            for(int i = 0; i < goals.Length; i++){ 
                if(!goals[i].completed)
                    return false; //se um dos Goals nao estiver completo, esse outcome nao aconteceu
            }

            completed = true; //passou o for por todos os Goals estarem completos
            return true;
        }
        
    }

    public Outcome[] outcomes;

    [Header("Force Map Gen")]
    public GameObject forcedRoom; 
    [Min(0)]public int desiredRoomQueuePosition = 0;

    public Outcome GetCompletedOutcome(){
        for(int i = 0; i < outcomes.Length; i++){
            if(outcomes[i].completed){
                return outcomes[i];
            }
        }
        return null;
    }

    public void ResolveCompletedCounter(MissionCounter counter){
        if(!isActive) return;
        for(int i = 0; i < outcomes.Length; i++){
            for(int y = 0; y < outcomes[i].goals.Length; y++){
                if(outcomes[i].goals[y].goalCounter == counter){
                    outcomes[i].goals[y].completed = true;
                }
            }
            if(outcomes[i].CheckGoals()){
                CompleteOutcome(i);
            }
        }
        parentQuestArc.questUpdateEvent.Raise();
    }

    public void CompleteOutcome(int outcomeIndex){// para completar outcome manualmente
        if(GetCompletedOutcome() != null || !isActive) return; //evita escolher outro outcome depois que um ja escolhido
        outcomes[outcomeIndex].completed = true;
        isActive = false;
        // atualizar quest arc
        if(outcomes[outcomeIndex].nextMission != null){
            parentQuestArc.NextMission(outcomes[outcomeIndex].nextMission);
        }else{
            parentQuestArc.CompleteQuest();
        }
        parentQuestArc.questUpdateEvent.Raise();
    }
}
