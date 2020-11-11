using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerPickupPanel : MonoBehaviour
{
    public PlayerCharacterSettings player;
    public Color targetDescriptionColor;
    public Color otherDescriptionColor;
    [Header("Componentes")]
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI augmentText;
    public PowerPickup pickup;

    private void Start() {
        UpdatePowerPickupPanel();  
    }

    private void OnEnable() {
        UpdatePowerPickupPanel();
    }

    public void UpdatePowerPickupPanel(){
        //skillText.text = "Unlock\n"
        bool playerHasRace = false;
        for (int i = 0; i < player.astralMapRaces.Length; i++)
        {
            if(player.astralMapRaces[i] == pickup.raceToPickUp){
                playerHasRace =true;
            }
        }

        skillText.color = playerHasRace ? otherDescriptionColor : targetDescriptionColor;
        augmentText.color = playerHasRace ? targetDescriptionColor : otherDescriptionColor;
    }
}
