using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHUDList : MonoBehaviour
{
    public QuestGroup[] targetGroups;
    public PoolContainer pooler;
    public PoolObject hudItem;
    public RectTransform itemContainer;

    [Header("Open Journal Settings")]
    public TabMenu tabMenu;
    public PauseMenu pauseMenu;
    public QuestMenu questMenu;

    private List<QuestHUDItem> itemList = new List<QuestHUDItem>();

    private void Start() {
        UpdateQuestList();
    }

    public void OpenJournalAndSelect(QuestArc targetQuest){
        if(Time.timeScale == 0) return; //player seeing another menu
        pauseMenu.PauseGame(true);
        tabMenu.SelectContent(questMenu.gameObject);
        questMenu.SearchAndSelectQuest(targetQuest);
    }

    public void UpdateQuestList(){//evento
        RemoveCompletedItems();
        for (int i = 0; i < targetGroups.Length; i++)
        {
            for (int y = 0; y < targetGroups[i].quests.Count;y++)
            {
                if(!targetGroups[i].quests[y].rewarded)
                    TryToInsertItem(targetGroups[i].quests[y]);
            }
        }
    }

    private void TryToInsertItem(QuestArc targetQuest){
        for(int i = itemList.Count - 1; i >= 0; i--){
            if(itemList[i].targetQuest == targetQuest){
                itemList[i].UpdateItem(targetQuest,this);
                return;
            }
        }
        QuestHUDItem item = pooler.SpawnTargetObject(hudItem,10,itemContainer).GetComponent<QuestHUDItem>();
        itemList.Add(item);
        item.UpdateItem(targetQuest,this);
    }

    private void RemoveCompletedItems(){
        for(int i = itemList.Count - 1; i >= 0; i--){
            if(itemList[i].targetQuest.rewarded){
                itemList[i].gameObject.SetActive(false);
                itemList.Remove(itemList[i]);
            }
        }
    }
}
