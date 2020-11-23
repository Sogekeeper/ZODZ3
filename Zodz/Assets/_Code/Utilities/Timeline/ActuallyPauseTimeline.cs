using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActuallyPauseTimeline : MonoBehaviour
{
    public PlayableDirector director;

    public void PauseTarget(){
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void ResumeTarget(){
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
