using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hellmade.Sound;

public class EasySoundController : MonoBehaviour
{
	public Slider globalMusicVolSlider;
	public Slider globalSoundVolSlider;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void GlobalMusicVolumeChanged()
	{
		EazySoundManager.GlobalMusicVolume = globalMusicVolSlider.value;
	}

	public void GlobalSoundVolumeChanged()
	{
		EazySoundManager.GlobalSoundsVolume = globalSoundVolSlider.value;
	}
}
