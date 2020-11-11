using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Hellmade.Sound;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Main Menu bit")]
    public Race lionRace;
    public WorldSettings worldSettings;
    public PlayerCharacterSettings playerCharacterSettings;

    [Header("For The training bit")]
    public int dummyHitsToTrigger;

    [Header("For the Roguestar Rift scene")]
    public QuestArc questToComplete;
    public string menuSceneString;
    public AudioClip punchSound;
    
    
    private int dummyCount;
    private bool dummyTriggered = false;

    public UnityEvent OnDummyHits;

    public void InitTutorial(){
        playerCharacterSettings.DebugResetCharacter();
        playerCharacterSettings.solarRace = lionRace;
        playerCharacterSettings.ascendantRace = lionRace;
        playerCharacterSettings.lunarRace = lionRace;
        playerCharacterSettings.SetupCharacter();
        worldSettings.InitializeWorld(lionRace.startingLocation);
    }

    public void DummyWasHit(){
        dummyCount++;
        if(dummyCount >= dummyHitsToTrigger && !dummyTriggered){
            OnDummyHits?.Invoke();
            dummyTriggered = true;
        }
    }

    public void Part1Complete(){

    }

    public void PlayKnockoutSound(){
        if(punchSound) EazySoundManager.PlaySound(punchSound);
    }
    public void EndCutscene(){
        questToComplete.GetCurrentMission().CompleteOutcome(0);
        questToComplete.rewarded = true;
        SceneManager.LoadScene(menuSceneString);
    }

    
}
