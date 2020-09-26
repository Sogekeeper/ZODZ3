using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestHUD : MonoBehaviour
{
    //WIP - Possui macaquices para J2

    public QuestController questController;
    [Space()]
    public GameObject questHudCanvas;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;

    private Quest previousQuest;
    private int previousMissionID;

    private void Awake() {
        questHudCanvas.SetActive(false);
    }

    private void Update() {
        if(questController.activeQuest){
            if((previousQuest && previousQuest != questController.activeQuest) || !previousQuest){
                UpdateQuestInformation();
            }
            if(previousMissionID < questController.activeQuest.completedQuests){
                UpdateQuestInformation();
            }
        }else{
            questHudCanvas.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Tab) && questController.activeQuest){
            questHudCanvas.SetActive(!questHudCanvas.activeInHierarchy);
        }
    }

    private void UpdateQuestInformation(){
        questHudCanvas.SetActive(true);
        previousMissionID = questController.activeQuest.completedQuests;
        previousQuest = questController.activeQuest;
        questName.text = previousQuest.quests[previousQuest.completedQuests].questName;
        questDescription.text = previousQuest.quests[previousQuest.completedQuests].questDescription;
    }
}
