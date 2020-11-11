using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnDemand : MonoBehaviour
{
    public Transform targetPosition;

    public void TeleportTarget(Transform objectToTeleport){
        objectToTeleport.position = targetPosition.position;
    }

    public void TeleportPlayer(){
        FindObjectOfType<PlayerStats>().transform.position = targetPosition.position;
    }
}
