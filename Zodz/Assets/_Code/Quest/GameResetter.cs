using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResetter : MonoBehaviour
{
    public Quest[] questsToReset;
    public BoolVariable[] switchesToReset;

    public void ResetAllProgress(){
        if(questsToReset != null && questsToReset.Length > 0){
            for(int i = 0; i < questsToReset.Length; i++){
                questsToReset[i].ResetMissions();
            }
        }
        if(switchesToReset != null && switchesToReset.Length > 0){
            for(int i = 0; i < switchesToReset.Length; i++){
                switchesToReset[i].SetValueFunction(false);
            }
        }
    }
}
