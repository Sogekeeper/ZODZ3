using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBar : MonoBehaviour
{
    public PoolObject iconPrefab;
    public Transform iconContainer;
    public PoolContainer iconPooler;

    public void UpdateStateBar(EntityStats owner){
        for(int i = 0; i < iconContainer.childCount;i++){
            iconContainer.GetChild(i).gameObject.SetActive(false);
        }
        if(owner.states == null && owner.states.Count <= 0) return;
        for(int i = 0; i < owner.states.Count; i++){
            for(int y = 0; y < owner.states[i].stackAmount; y++){
                if(owner.states[i].currentState.stateIcon == null) continue;
                StateIcon ic = iconPooler.SpawnTargetObject(iconPrefab,10,iconContainer).GetComponent<StateIcon>();
                //ic.transform.parent = iconContainer;
                ic.SetUpIcon(owner.states[i].currentState.stateIcon);
            }
        }
    }
}
