using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManaBar : MonoBehaviour
{
    public Image barFill;
    public EntityStats targetEntity;

    [Header("Optional")]
    public Image backBarFill;
    public float backBarDampTime = 0.3f;
    public Text amountText;

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
}
