using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    public float blinkRate;
    public float blinkDuration;
    public Image defaultTarget;
    public bool endActive = false;

    bool blinking = false;
    float blinkTimer;
    float blinkTotalTimer;
    Image currentTarget;

    private void Update() {
        if(blinkTotalTimer > 0){
            blinkTotalTimer -= Time.deltaTime;
            if(blinkTotalTimer <= 0){
                StopBlinking();
            }
        }
        if(blinkTimer > 0){
            blinkTimer -= Time.deltaTime;
            if(blinkTimer <= 0){
                if(currentTarget) currentTarget.enabled = !currentTarget.enabled;
                if(blinking) blinkTimer = blinkRate;
            }
        }
    }

    public void BeginBlinking(){
        currentTarget = defaultTarget;
        blinkTimer = blinkRate;
        blinkTotalTimer = blinkDuration;
        blinking = true;
    }

    public void StopBlinking(){
        blinkTimer =0;
        blinkTotalTimer = 0;
        currentTarget.enabled = endActive;
        blinking = false;
    }

}
