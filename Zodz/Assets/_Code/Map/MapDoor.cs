﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDoor : MonoBehaviour
{
    public GameObject doorImageObject; //PLACEHOLDER
    public Collider2D interactableTrigger;
    [HideInInspector]public MapRoomGenerator generator;

    private bool _locked;

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
        generator.AdvanceRoom();
    }
}
