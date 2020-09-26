using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolDependingEvent : MonoBehaviour
{
    public BoolVariable targetSwitch;

    public UnityEvent FalseResult;
    public UnityEvent TrueResult;

    public void TriggerResult(){
        if(targetSwitch.Value){
            TrueResult?.Invoke();
        }else{
            FalseResult?.Invoke();            
        }
    }
}
