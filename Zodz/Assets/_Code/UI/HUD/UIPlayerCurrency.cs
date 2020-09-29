using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerCurrency : MonoBehaviour
{
    public PlayerCharacterSettings playerSettings;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI crystalText;
    private int curCoins;
    private int curCrystals;

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
