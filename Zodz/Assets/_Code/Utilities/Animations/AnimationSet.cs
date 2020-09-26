using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Animation_Set", menuName = "Animations/Animation Set", order = 1)]
public class AnimationSet : ScriptableObject
{
    public AnimationClip upClip;
    public AnimationClip downClip;
    public AnimationClip rightClip;
    public AnimationClip leftClip;
}
