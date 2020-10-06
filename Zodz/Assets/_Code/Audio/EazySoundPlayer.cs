using UnityEngine;
using Hellmade.Sound;

public class EazySoundPlayer : MonoBehaviour
{
    public bool isUI = false;
    public AudioClip targetAudio;
    public float baseVolume = 1;

    public void PlayThisSound(){
        if(isUI)EazySoundManager.PlayUISound(targetAudio,baseVolume);
        else EazySoundManager.PlaySound(targetAudio,baseVolume);
    }

}
