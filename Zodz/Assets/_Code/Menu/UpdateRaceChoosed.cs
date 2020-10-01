using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateRaceChoosed : MonoBehaviour
{
	public bool playerAlreadyChoosed;
	public Race raceDefault;
	public TextMeshProUGUI nameRace;
	public Image skillImage1;
	public Image skillImage2;
	public Image skillImage3;
	public TextMeshProUGUI skillText1;
	public TextMeshProUGUI skillText2;
	public TextMeshProUGUI skillText3;
	public TextMeshProUGUI skillDescription1;
	public TextMeshProUGUI skillDescription2;
	public TextMeshProUGUI skillDescription3;
	public Animator animatorPlayer;
	public Image RaceLogo;
	public AnimatorOverrideController userAnimOverride;
	public AnimationClipOverrides clipOverrides;

	private void Awake()
	{
		clipOverrides = new AnimationClipOverrides(userAnimOverride.overridesCount);
		userAnimOverride.GetOverrides(clipOverrides);
	}

	private void OnEnable()
	{
		if(playerAlreadyChoosed)
			raceDefault = PlayerRaceChoice.playerRaceChoice;
		nameRace.text = raceDefault.name;
		skillImage1.sprite = raceDefault.basicSkill.skillIcon;
		skillImage2.sprite = raceDefault.magicSkill.skillIcon;
		skillImage3.sprite = raceDefault.ultimateSkill.skillIcon;
		skillText1.text = raceDefault.basicSkill.skillName;
		skillText2.text = raceDefault.magicSkill.skillName;
		skillText3.text = raceDefault.ultimateSkill.skillName;
		skillDescription1.text = raceDefault.basicSkill.skillShortDescription;
		skillDescription2.text = raceDefault.magicSkill.skillShortDescription;
		skillDescription3.text = raceDefault.ultimateSkill.skillShortDescription;
		RaceLogo.sprite = raceDefault.raceIcon;
		ReplaceDisplayAnimationSet(raceDefault.displayAnimation);


	}

	public void ReplaceDisplayAnimationSet(AnimationDisplay runSet)
	{
		if (runSet == null) return;
		clipOverrides["Leo_Idle_right"] = runSet.idleClip;
		clipOverrides["Leo_Walk_right"] = runSet.runClip;
		userAnimOverride.ApplyOverrides(clipOverrides);
	}
}
