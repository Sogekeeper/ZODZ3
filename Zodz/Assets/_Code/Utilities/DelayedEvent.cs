using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
    public bool callOnStart = false;
    public bool isInstant = false;
    public float secondsToWait = 2f;
    public UnityEvent OnTimerEnded;

    private float timer;

    private void Start()
    {
        if(callOnStart){
            StartTimer();
        }
    }

    private void Update() {
        if(timer > 0){
            timer -= Time.deltaTime;
            if(timer <= 0)
                OnTimerEnded?.Invoke();
        }
    }

    public void StartTimer(){
        if(!isInstant){
            timer = secondsToWait;            
        }
        else{
            OnTimerEnded?.Invoke();
        }
    }
    
}
