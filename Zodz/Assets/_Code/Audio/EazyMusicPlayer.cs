using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class EazyMusicPlayer : MonoBehaviour
{
    public float relativeVolume = 0.5f;
    public AudioClip targetMusic;
    public bool looping = true;
    public bool persist = false;
    public bool playerOnStart = true;

    private void Start(){
        EazySoundManager.PlayMusic(targetMusic,relativeVolume,looping,persist);
    }
}
