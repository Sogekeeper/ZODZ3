using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AugmentMenuItem : MonoBehaviour
{
    public TextMeshProUGUI buttonDescription;
    private AugmentMenu augmentMenu;
    private AugmentOption targetAugment;

    public void SetupAugmentMenuItem(AugmentOption augment, AugmentMenu menu){
        targetAugment = augment;
        buttonDescription.text = augment.augmentDescription;
        augmentMenu = menu;
    }

    public void PickAugment(){
        augmentMenu.PickAugment(targetAugment);
    }

}
