using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSoundChanger : MonoBehaviour
{
    public AudioClip singleAudioChange;

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerRunningAudio m = other.GetComponent<PlayerRunningAudio>();
        if(m) m.ChangeSingleAudio(singleAudioChange);
    }
}
