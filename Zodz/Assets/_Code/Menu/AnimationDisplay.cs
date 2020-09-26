using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Animation_Display", menuName = "Animations/Animation Display")]
public class AnimationDisplay : ScriptableObject
{
	public AnimationClip idleClip;
	public AnimationClip runClip;
}
