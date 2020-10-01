using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestGroup", menuName = "Questing/Quest Group", order = 3)]
public class QuestGroup : ScriptableObject
{
    public string groupName = "Main Quests";
    public List<QuestArc> quests = new List<QuestArc>();

    public void BeginNewQuest(QuestArc targetQuest){
        quests.Add(targetQuest);
        targetQuest.InitializeQuest();
        targetQuest.questUpdateEvent.Raise();
    }
}
