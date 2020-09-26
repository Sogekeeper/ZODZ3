using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuItem : MonoBehaviour
{
    public Text itemText;
    public Button itemButton;
    public QuestArc targetQuest;
    public RectTransform rect;

    private QuestMenu targetMenu;

    private void OnDisable() {
        itemButton.onClick.RemoveAllListeners();
    }

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    public void SetupItem(QuestArc quest, QuestMenu questMenu){
        targetQuest = quest;
        targetMenu = questMenu;
        if(quest.rewarded)itemText.text = "<s>"+quest.questName+"</s>";
        else itemText.text = quest.questName;
    }

    public void SelectQuest(){
        targetMenu.ViewQuest(targetQuest);
    }
}
