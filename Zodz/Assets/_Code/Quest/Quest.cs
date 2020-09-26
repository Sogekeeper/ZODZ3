using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OLD
[CreateAssetMenu(fileName = "Quest", menuName = "Questing/Quest", order = 3)]
public class Quest : ScriptableObject
{
    public string questArcName;

    [System.Serializable]
    public class QuestInfo{
        public string questName;
        [TextArea]public string questDescription;
        public Map forcedMap;
        [Header("To set elsewhere")]
        public Transform currentObjective;
    }

    public int completedQuests=0;
    public QuestInfo[] quests;

    public void NextQuest(){
        completedQuests++;
    }

    public void ResetMissions(){
        completedQuests = 0;
    }
}

