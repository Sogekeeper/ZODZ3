using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    public Race raceToPickUp;
    public Collider2D pickupCollider;
    public SpriteRenderer pickupRenderer;
    [Header("Optional Random PickUp")]
    public bool useRandomRace;
    public Race[] possibleRaces;

    private void Start() {
        pickupCollider.enabled = true;
    }

    public void UpdateToPossibleRace(){
        if(!useRandomRace) return;
        int randomIndex = Random.Range(0,possibleRaces.Length);
        randomIndex = randomIndex % possibleRaces.Length; //so pra ter certeza
        raceToPickUp = possibleRaces[randomIndex];
        pickupRenderer.sprite = possibleRaces[randomIndex].raceIcon;
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
