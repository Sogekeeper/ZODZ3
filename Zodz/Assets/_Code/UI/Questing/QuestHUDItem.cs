using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestHUDItem : MonoBehaviour
{
    public Text questLabel;
    public TextMeshProUGUI buttonText;
    public LayoutElement layoutElement;
    public Color defaultButtonColor;
    public Color completedButtonColor;
    public QuestArc targetQuest {get;private set;}
    private QuestHUDList owner;

    public void UpdateItem(QuestArc target,QuestHUDList hudList){
        targetQuest = target;
        owner = hudList;
        questLabel.text = target.questName;
        if(target.completed){
            buttonText.gameObject.SetActive(false);
            buttonText.text = "Completed. Click here for Rewards.";
            buttonText.ForceMeshUpdate();
            buttonText.gameObject.SetActive(true);
            buttonText.color = completedButtonColor;
        }else{
            Mission m = target.GetCurrentMission();
            if(m == null){
                Debug.Log("Ongoing QuestArc does not have active missions: "+target.questName);
            }else{
                buttonText.gameObject.SetActive(false);
                buttonText.text="";
                for(int i = 0; i < m.outcomes.Length;i++){
                    for(int y = 0; y < m.outcomes[i].goals.Length; y++){
                        buttonText.text += m.outcomes[i].goals[y].shortDescription;
                        if(m.outcomes[i].goals[y].showCounter)
                            buttonText.text += " "+m.outcomes[i].goals[y].goalCounter.currentAmount+"/"+m.outcomes[i].goals[y].goalCounter.targetAmount;
                        if(y < m.outcomes[i].goals.Length -1) buttonText.text += "\n";
                    }
                    if(i < m.outcomes.Length -1) buttonText.text += "\nor\n";
                }
                buttonText.color = defaultButtonColor;
                buttonText.ForceMeshUpdate();
                buttonText.gameObject.SetActive(true);
            }
        }
        Vector2 textSize = buttonText.GetPreferredValues(buttonText.text);
        buttonText.rectTransform.sizeDelta = textSize;
        layoutElement.preferredHeight = buttonText.rectTransform.rect.height;
    }
    public void OpenJournal(){
        owner?.OpenJournalAndSelect(targetQuest);
    }
}
