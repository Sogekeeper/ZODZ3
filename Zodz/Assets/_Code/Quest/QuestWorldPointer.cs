using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWorldPointer : MonoBehaviour
{
    public QuestController controller;
    public SpriteRenderer visual;

    private void Update() {
        if(controller.activeQuest && controller.activeQuest.quests[controller.activeQuest.completedQuests].currentObjective){
            visual.enabled = true;
            Vector3 direction = controller.activeQuest.quests[controller.activeQuest.completedQuests].currentObjective.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = rot;
        }else{
            visual.enabled = false;
        }
    }
}
