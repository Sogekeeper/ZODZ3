using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDoor : MonoBehaviour
{
    public GameObject doorImageObject; //PLACEHOLDER
    public Collider2D interactableTrigger;
    public SpriteRenderer doorIcon;
    [HideInInspector]public MapRoomGenerator generator;
    [HideInInspector]public Reward currentReward;

    private bool _locked;

    public void SetupDoor(MapRoomGenerator targetGenerator, Reward targetReward){
        currentReward = targetReward;
        generator = targetGenerator;
        if(doorIcon && targetReward && targetReward.rewardDoorImage){
            doorIcon.sprite = targetReward.rewardDoorImage;
        }
        //colocar sprite na porta
    }

    public void SetLocked(bool locked){ //para usar no gerador
        _locked = locked;
        if(locked){
            interactableTrigger.enabled = false;
            doorImageObject.gameObject.SetActive(true);
        }else{
            interactableTrigger.enabled = true;
            doorImageObject.gameObject.SetActive(false);
        }
    }

    public void EnterDoor(){ //pra usar com o interactable
        if(!generator || _locked) return;
        generator.AdvanceRoom(currentReward);
        //setar recompensa
    }
}
