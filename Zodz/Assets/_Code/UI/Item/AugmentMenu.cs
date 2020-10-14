using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentMenu : MonoBehaviour
{
    public PlayerCharacterSettings playerCharacterSettings;
    public PoolContainer pooler;
    public PoolObject augmentMenuItem;
    public Transform augmentPanelsContainer;
    public TextMeshProUGUI menuHeader;

    private int mapIndex;
    private PlayerStats playerStats;

    public void SetupMenu(Race raceToUpgrade, int astralMapIndex, PlayerStats player){
        if(raceToUpgrade.augments == null || raceToUpgrade.augments.Length <= 0) return;
        playerStats = player;
        for (int i = 0; i < augmentPanelsContainer.childCount; i++)
        {
            augmentPanelsContainer.GetChild(i).gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        for (int i = 0; i < raceToUpgrade.augments.Length; i++)
        {
            AugmentMenuItem menuItem;
            menuItem = pooler.SpawnTargetObject(augmentMenuItem,5,augmentPanelsContainer).GetComponent<AugmentMenuItem>();
            menuItem.SetupAugmentMenuItem(raceToUpgrade.augments[i],this);            
        }
        menuHeader.text = "Choose an augment for the "+raceToUpgrade.magicSkill.skillName+" skill.";
        Time.timeScale = 0;
    }

    public void PickAugment(AugmentOption optionPicked){
        // resolve augment and close menu
        optionPicked.UpgradeMultipliers();
        playerCharacterSettings.augmentsPicked[mapIndex] = optionPicked;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    
}
