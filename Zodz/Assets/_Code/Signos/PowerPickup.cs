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
    public PlayerCharacterSettings playerCharacterSettings;    

    private void Start() {
        pickupCollider.enabled = true;
    }

    public void UpdateToPossibleRace(){
        if(!useRandomRace) return;
        int randomIndex = Random.Range(0,playerCharacterSettings.possibleRaces.Length);
        randomIndex = randomIndex % playerCharacterSettings.possibleRaces.Length; //so pra ter certeza
        raceToPickUp = playerCharacterSettings.possibleRaces[randomIndex];
        pickupRenderer.sprite = playerCharacterSettings.possibleRaces[randomIndex].raceIcon;
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
