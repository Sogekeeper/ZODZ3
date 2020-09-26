using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventByTriggerEnter : MonoBehaviour
{
    public bool repeat = false;
    public bool repeatTimer = false;
    public float timeToWaitInside = 0;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnTimerStay;
    public List<UnityEvent> otherVisits;

    private int timesEntered = 0;
    private bool doneIn = false;
    private bool doneOut = false;
    private bool doneInside = false;
    private bool isInside = false;
    private float insideTimer = 0;

    private void Start() {
        if(otherVisits == null) otherVisits = new List<UnityEvent>();
    }
    

    private void Update() {
        if(insideTimer < timeToWaitInside && isInside){
            insideTimer += Time.deltaTime;
            if(insideTimer >= timeToWaitInside && (!doneInside || repeatTimer)){
                OnTimerStay.Invoke();
                doneInside = true;
                insideTimer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Trigger: "+other.gameObject);
        if(other.CompareTag("Player")){
            isInside = true;
        }
        if(other.CompareTag("Player") && (!doneIn || repeat)){
            OnEnter.Invoke();
            doneIn = true;
        }
        else if(other.CompareTag("Player") && otherVisits.Count > 1){
            if(timesEntered >= otherVisits.Count) return;
            otherVisits[timesEntered].Invoke();
            timesEntered++;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            insideTimer = 0;
            isInside = false;
            if((!doneOut || repeat)){
                OnExit.Invoke();
                doneOut = true;
            }
        }
    }
    
}
