using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDeactivation : MonoBehaviour
{
    public bool startOnEnable = false;
    public float seconds = 0.2f;

    private float timer = 0;

    private void OnEnable() {
        StartTime();
    }

    private void Update() {
        if(timer > 0){
            timer -= Time.deltaTime;
            if(timer <= 0){
                gameObject.SetActive(false);
            }
        }
    }

    public void StartTime(){
        timer = seconds;
    }
}
