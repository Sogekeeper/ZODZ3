using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Race_Anim_Clip", menuName = "Animations/Race Anim Clip", order = 0)]
public class RaceDependingAnimation : ScriptableObject
{
  [System.Serializable]
  public class AnimationInfo{
    public Race animationOwner;
    public AnimationClip targetClip; 
  }

  public AnimationInfo[] possibleClips;

  public AnimationClip GetClipForRace(Race targetRace){
    AnimationClip resultClip = null;
    if(possibleClips != null && possibleClips.Length > 0){
      for(int i = 0; i < possibleClips.Length; i++){
        if(possibleClips[i].animationOwner == targetRace){
          resultClip = possibleClips[i].targetClip;
        }
      }
    }
    if(resultClip == null){
      Debug.LogError("Busca por Clip sem resultado");
      return resultClip;
    }
    return resultClip;
  }
}
