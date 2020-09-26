using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuestMenu : MonoBehaviour
{
    public TextMeshProUGUI questDescription;
    public PauseMenu menu;
    public QuestMenuCategory[] categories;

    [Header("Complete Button")]
    public Button completeButton;
    public Text completeText;
    public Color toCompleteTextColor;
    public Color completedTextColor;

    private QuestArc selectedQuest;

    private void Start(){
        UpdateCategories();
        completeButton.interactable = false;
        completeText.text = "COMPLETED";
        completeText.color = completedTextColor;
    }

    public void UpdateCategories(){
        for(int i = 0; i < categories.Length;i++){
            categories[i].UpdateCategory();
        }
    }

    public bool SearchAndSelectQuest(QuestArc target){
        for(int i = 0; i < categories.Length;i++){
            QuestMenuItem item = categories[i].SearchItem(target,true); //method will open category if found
            if(item != null){
                ViewQuest(item.targetQuest);
                EventSystem.current.SetSelectedGameObject(item.itemButton.gameObject);
                return true;
            }               
        }
        return false;
    }

    public void CompleteQuest(){
        //spawnar recompensas
        selectedQuest.Reward();
        menu.PauseGame();//toggle
    }

    public void ViewQuest(QuestArc target){
        selectedQuest = target;
        questDescription.text=
        "<size=200%>"+selectedQuest.questName+
        "\n<size=100%>"+selectedQuest.questDescription+
        "\n<indent=15%>";

        for(int i =0; i < selectedQuest.missions.Count; i++){
            Mission m = selectedQuest.missions[i];
            if(!m.isActive){ //if is on QuestArc and it's not active, it's already completed
                //show goals from completed outcome
                Mission.Outcome outcome = m.GetCompletedOutcome();
                if(outcome == null){
                    questDescription.text += "\n- Error</indent>";
                    break;
                }
                for(int y = 0; y < outcome.goals.Length; y++){
                    questDescription.text += "\n<s>- "+outcome.goals[y].shortDescription;
                    if(outcome.goals[y].showCounter)
                        questDescription.text += " "+outcome.goals[y].goalCounter.currentAmount+"/"+outcome.goals[y].goalCounter.targetAmount;
                    questDescription.text += "</s>";
                }
            }else{//is the current not completed mission
                for(int y = 0; y < m.outcomes.Length; y++){
                    for(int yi = 0; yi < m.outcomes[y].goals.Length; yi++){
                        questDescription.text += "\n- "+m.outcomes[y].goals[yi].shortDescription;
                        if(m.outcomes[y].goals[yi].showCounter)
                            questDescription.text += " "+m.outcomes[y].goals[yi].goalCounter.currentAmount+"/"+m.outcomes[y].goals[yi].goalCounter.targetAmount;  
                    }
                    if(y < m.outcomes.Length - 1){
                        questDescription.text += "\nor";
                    }
                }
            }
        }
        completeButton.interactable = (selectedQuest.completed && !selectedQuest.rewarded);
        if(completeButton.interactable){
            completeText.text = "COMPLETE";
            completeText.color = toCompleteTextColor;
        }else{
            if(selectedQuest.rewarded)
                completeText.text = "COMPLETED";
            else
                completeText.text = "IN PROGRESS";
            completeText.color = completedTextColor;
        }
    }
    
    
}
