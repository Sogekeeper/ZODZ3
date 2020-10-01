using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerCurrency : MonoBehaviour
{
    public PlayerCharacterSettings playerSettings;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI crystalText;
    private int curCoins =-1;
    private int curCrystals =-1;
    
    private void Start() {
        curCoins = playerSettings.coins;
        coinText.SetText(curCoins.ToString());
        curCrystals = playerSettings.crystals;
        crystalText.SetText(curCrystals.ToString());
    }

    private void Update() {
        if(playerSettings.coins != curCoins){
            curCoins = playerSettings.coins;
            coinText.SetText(curCoins.ToString());
        }
        if(playerSettings.crystals != curCrystals){
            curCrystals = playerSettings.crystals;
            crystalText.SetText(curCrystals.ToString());
        }
    }
}
