using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hellmade.Sound;

public class UIManaBar : MonoBehaviour
{
    public Image barFill;
    public EntityStats targetEntity;

    [Header("Optional")]
    public Image backBarFill;
    public float backBarDampTime = 0.3f;
    public Text amountText;
    public Image missingManaBar;
    public BlinkUI missingManaBlink;
    public AudioClip missingManaSound;

    private float velor;

    private void Update() {
        UpdateBarFill();
    }

    public void UpdateBarFill(){
        barFill.fillAmount = (float)targetEntity.currentMana/targetEntity.totalMana.Value;
        if(backBarFill)backBarFill.fillAmount = Mathf.SmoothDamp(backBarFill.fillAmount,
            barFill.fillAmount,ref velor, backBarDampTime);
        if(amountText)amountText.text = targetEntity.currentMana.ToString()+
            "/"+targetEntity.totalMana.Value.ToString();
    }

    public void BlinkNotEnoughMana(Skill skill){
        missingManaBar.fillAmount = (float)skill.skillCost/targetEntity.totalMana.Value;
        missingManaBlink.BeginBlinking();
        if(missingManaSound) EazySoundManager.PlayUISound(missingManaSound,0.2f);
    }
}
