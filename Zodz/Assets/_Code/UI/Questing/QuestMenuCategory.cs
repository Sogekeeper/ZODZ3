using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuCategory : MonoBehaviour
{
    [Header("General Settings")]
    public QuestGroup questsToDisplay;
    public bool displayActive = true;
    public bool displayCompleted = false;
    public QuestMenu questMenu;
    public PoolObject itemPrefab;
    public PoolContainer pooler;
    [Header("Dropdown Settings")]
    public float verticalOffset = 5f;
    public float horizontalOffset = 10f;

    private RectTransform container;
    private float originalHeight;
    private bool open = false;
    private List<QuestMenuItem> itemList = new List<QuestMenuItem>();
    private LayoutElement layout;

    private void Awake() {
        if(itemList == null)itemList = new List<QuestMenuItem>();
        layout = GetComponent<LayoutElement>();
        container = GetComponent<RectTransform>();
        originalHeight = container.rect.height;
    }  

    private void Update() {
        if(layout != null && layout.preferredHeight != container.rect.height)
            layout.preferredHeight = container.rect.height;
    }

    public QuestMenuItem SearchItem(QuestArc targetQuest, bool openIfFound = false){
        for(int i = 0; i < itemList.Count;i++){
            if(itemList[i].targetQuest == targetQuest){
                if(openIfFound && !open)
                    Open(false);
                return itemList[i];
            }
        }
        return null;
    }

    public void Open(bool instant = false,bool toggle = true){
        if(toggle) open = !open; //disabling toggle allows me to call this method to update the size of the container when it's already open
        float itemHeight = itemPrefab.GetComponent<QuestMenuItem>().rect.rect.height;
        if(open){
            EnableItems();            
            if(instant)
                container.LeanSize(new Vector2(container.rect.width,originalHeight+(itemHeight+verticalOffset)*itemList.Count),0.02f)
                .setEaseInOutCubic().setIgnoreTimeScale(true);
            else
                container.LeanSize(new Vector2(container.rect.width,originalHeight+(itemHeight+verticalOffset)*itemList.Count),0.3f)
                .setEaseInOutCubic().setIgnoreTimeScale(true);
        }else{ //fechar
            //TO DO: Prevent selecting the items while the list is disappearing 
            if(instant)
                container.LeanSize(new Vector2(container.rect.width,originalHeight),0.02f)
                .setEaseInOutCubic().setOnComplete(DisableItems).setIgnoreTimeScale(true);
            else
                container.LeanSize(new Vector2(container.rect.width,originalHeight),0.3f)
                .setEaseInOutCubic().setOnComplete(DisableItems).setIgnoreTimeScale(true);
            
        }
    } 

    public void Open(){
        Open(false,true);
    }

    private void DisableItems(){
        for(int i = itemList.Count -1; i >= 0 ;i--){
            itemList[i].gameObject.SetActive(false);
        }
    }
    private void EnableItems(){
        for(int i = itemList.Count -1; i >= 0 ;i--){
            itemList[i].gameObject.SetActive(true);
        }
    }

    public void UpdateCategory(){
        float containerHeight = originalHeight;            
        float itemHeight = itemPrefab.GetComponent<QuestMenuItem>().rect.rect.height;
        float currentY = (containerHeight + verticalOffset) * -1;

        //if player completed a quest and it should no longer be here, remove it
        for(int i = itemList.Count -1; i >= 0;i--){
            QuestMenuItem item = itemList[i];
            if((!displayCompleted && item.targetQuest.rewarded) || (!displayActive && !item.targetQuest.rewarded)){
                itemList.Remove(item);
                item.gameObject.SetActive(false);
            }
        }

        if(questsToDisplay.quests == null) return;

        //check if quest to add is already here and if not, add it
        for(int i = 0; i < questsToDisplay.quests.Count;i++){
            if((displayActive && !questsToDisplay.quests[i].rewarded) || (displayCompleted && questsToDisplay.quests[i].rewarded)){
                bool found = false;
                for(int y = 0; y < itemList.Count;y++){
                    if(itemList[y].targetQuest == questsToDisplay.quests[i]){
                        found = true;
                    }
                }
                if(!found){
                    Debug.Log("Created Quest Menu Item");
                    QuestMenuItem item = pooler.SpawnTargetObject(itemPrefab,10,transform).GetComponent<QuestMenuItem>();
                    item.rect.anchoredPosition = new Vector2(horizontalOffset,currentY);
                    itemList.Add(item);
                    item.SetupItem(questsToDisplay.quests[i],questMenu);
                    item.gameObject.SetActive(false);
                }
            }
        }

        //update positions for active quests that always appear before completed ones
        if(displayActive){
            for(int i = itemList.Count -1; i >= 0 ;i--){
                if(!itemList[i].targetQuest.rewarded){
                    itemList[i].gameObject.SetActive(true);
                    itemList[i].rect.anchoredPosition = new Vector2(horizontalOffset,currentY);
                    currentY -= itemHeight + verticalOffset;
                }
            }
        }
        //update pos for completed
        if(displayCompleted){
            for(int i = itemList.Count -1; i >= 0 ;i--){
                if(itemList[i].targetQuest.rewarded){
                    itemList[i].gameObject.SetActive(true);
                    itemList[i].rect.anchoredPosition = new Vector2(horizontalOffset,currentY);
                    currentY -= itemHeight + verticalOffset;
                }
            }
        }

        if(open){
            Open(true,false);
        }
    }
}
