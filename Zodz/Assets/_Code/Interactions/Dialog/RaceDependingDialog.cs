using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog_RaceBased", menuName = "Interactions/Race Depending Dialog", order = 3)]
public class RaceDependingDialog : ScriptableObject
{
    [System.Serializable]
    public class RaceDialog{
        public Race[] targetRaces;
        public BubbleDialogSequence.LineInfo[] lines;
    }

    public BubbleDialogSequence.LineInfo[] defaultLines;
    public RaceDialog[] possibleDialogs;

    public BubbleDialogSequence.LineInfo[] GetRaceLines(Race race){
        if(possibleDialogs == null) return null;
        for(int i = 0; i < possibleDialogs.Length; i++){
            for(int y = 0; y < possibleDialogs[i].targetRaces.Length; y++){
                if(possibleDialogs[i].targetRaces[y] == race){
                    return possibleDialogs[i].lines;
                }
            }
        }
        return defaultLines;
    }
}
