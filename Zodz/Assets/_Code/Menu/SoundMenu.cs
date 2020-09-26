using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using System;

public class SoundMenu : MonoBehaviour
{
	private int BGMusicID;
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
		BGMusicID = EazySoundManager.PlayMusic(audios[0].audioClip, 1, true, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void PlayClickSound()
	{
		EazySoundManager.PlaySound(audios[1].audioClip);
	}

}
