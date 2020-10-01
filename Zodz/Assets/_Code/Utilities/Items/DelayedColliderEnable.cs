using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedColliderEnable : MonoBehaviour
{
    public Collider2D thisCollider;
    public float secondsForCollider = 0.8f;

    public void StartColliderDelay(){
        StopAllCoroutines();
        StartCoroutine(WaitAndTurnColliderOn());
    }

    public IEnumerator WaitAndTurnColliderOn(){
        thisCollider.enabled = false;
        yield return new WaitForSeconds(secondsForCollider);
        thisCollider.enabled = true;
    }
}
