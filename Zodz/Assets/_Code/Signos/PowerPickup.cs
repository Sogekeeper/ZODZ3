using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    public Race raceToPickUp;
    public Collider2D pickupCollider;

    private void Start() {
        pickupCollider.enabled = true;
    }
    
    public void PickingUp(AbilityToInteract actor){
        PlayerStats ps = actor.GetComponent<PlayerStats>();
        if(ps != null){
            Debug.Log("New Sign added to Astral Map!");
            pickupCollider.enabled = false;
            ps.AddRaceToMap(raceToPickUp);
        }
    }
}
