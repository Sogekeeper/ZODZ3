using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class EazyMusicPlayer : MonoBehaviour
{
    public AudioClip targetMusic;
    public bool looping = true;
    public bool playerOnStart = true;

    private void Start(){
        EazySoundManager.PlayMusic(targetMusic,EazySoundManager.GlobalMusicVolume);
    }
}
