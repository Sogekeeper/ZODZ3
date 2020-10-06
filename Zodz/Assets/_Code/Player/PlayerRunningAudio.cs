using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using UnityEngine.InputSystem;

public class PlayerRunningAudio : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float baseVolume = 0.3f;
    public bool useSingleAudio = true;
    public AudioClip singleAudio;
    public float multipleAudioRate = 0.3f;
    public AudioClip[] multipleAudios;

    private float multipleAudioTimer;
    private Transform trans;
    private Audio currentAudio;
    private PlayerStats playerStats;
    private SkillUser skillUser;

    private void Awake(){
        trans = GetComponent<Transform>();
        skillUser = playerMovement.GetComponent<SkillUser>();
        playerStats = playerMovement.GetComponent<PlayerStats>();
        int curID = EazySoundManager.PlaySound(singleAudio,baseVolume,true,null);
        currentAudio = EazySoundManager.GetAudio(curID);
    }

    private void Update(){
        if(playerMovement.CheckIfCanUpdateAnimatorInput() && playerStats.canMove && !skillUser.usingSkill){
            if(useSingleAudio){
                currentAudio?.Resume();
            }else{

            }
        }else{//not walking            
            currentAudio?.Pause();            
        }
    }    

    public void ChangeSingleAudio(AudioClip targetAudio){
        currentAudio.Stop();
        currentAudio.Clip = targetAudio;
        currentAudio.Play();
    }
    
}
