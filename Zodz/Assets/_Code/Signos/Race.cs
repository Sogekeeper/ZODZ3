using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Race Sheet", menuName = "Race", order = 0)]
public class Race : ScriptableObject
{
	public string raceName = "Furry";
	public Sprite raceIcon;
	public Element raceElement;
	public string startingScene = "City_Leo";
	public Location startingLocation;
	public Quest startigQuest;

	public Skill basicSkill;
	public Skill magicSkill;
	public Skill ultimateSkill;

	public int strength = 5;
	public int mind = 5;
	public int constitution = 5;
	public int spirit = 5;

	public AugmentOption[] augments;

	[Header("Visual")]
	//public Sprite characterPreview;//sem utilidade ainda

	public AnimationSet runningAnimations;
	public AnimationSet idleAnimations;
	public AnimationSet dashAnimations;
	public AnimationSet damageAnimations;
	public AnimationClip deathAnim;
	public AnimationDisplay displayAnimation;


	public void ResetAugments(){
		if(augments == null) return;
        for (int i = 0; i < augments.Length; i++)
        {
            if(augments[i] == null) continue;
            augments[i].ResetMultipliers();
        }
	}
}
