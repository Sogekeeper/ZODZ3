using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Hellmade.Sound;

public class OptionsInGameSound : MonoBehaviour
{
	[Serializable]
	public class AudioClipsClass
	{
		public string nameAudioClip;
		public AudioClip audioClip;
	}
	public AudioClipsClass[] audios;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void PlayClickSound()
	{
		EazySoundManager.PlaySound(audios[0].audioClip);
	}
}
