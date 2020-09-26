using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public EntityRuntimeSet enemiesSet;
    public int targetAmount = 0;
    public UnityEvent OnReachTarget;

    [Header("Optional")]
    public TextMeshProUGUI textCounter;

    private void OnEnable() {
        enemiesSet.onChangeAmount += this.AmountChangedCallback;
    }

    private void OnDisable() {
        enemiesSet.onChangeAmount -= this.AmountChangedCallback;
    }

    public void AmountChangedCallback(){
        if(enemiesSet.Items.Count <= targetAmount){
            OnReachTarget?.Invoke();
            if(textCounter){
                textCounter.text = "Enemies Left: "+enemiesSet.Items.Count;
            }
        }
    }

}
