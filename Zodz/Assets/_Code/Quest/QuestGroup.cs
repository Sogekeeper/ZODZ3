using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestGroup", menuName = "Questing/Quest Group", order = 3)]
public class QuestGroup : ScriptableObject
{
    public string groupName = "Main Quests";
    public List<QuestArc> quests;
}
