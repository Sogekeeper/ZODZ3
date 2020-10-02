using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateLunarInfo : MonoBehaviour
{
	public Race raceToGetLunarInfo;
	public TextMeshProUGUI lunarInfo;
	public TextMeshProUGUI lunarTitle;
	public Image lunarIcon;
	public bool alreadyChoosed;

	private void OnEnable()
	{
		if (alreadyChoosed)
		{
			lunarIcon.sprite = PlayerLunarChoice.playerLunarChoice.raceIcon;

			lunarTitle.text = PlayerLunarChoice.playerLunarChoice.name + ":";

			lunarInfo.text = "Strengh = " + PlayerLunarChoice.playerLunarChoice.strength + " " +
							 "Mind = " + PlayerLunarChoice.playerLunarChoice.mind + " \n" +
							 "Constitution = " + PlayerLunarChoice.playerLunarChoice.constitution + " " +
							 "Spirit = " + PlayerLunarChoice.playerLunarChoice.spirit;
		}
		else
		{
			lunarIcon.sprite = raceToGetLunarInfo.raceIcon;

			lunarTitle.text = raceToGetLunarInfo.name + ":";

			lunarInfo.text = "Strengh = " + raceToGetLunarInfo.strength + " " +
							 "Mind = " + raceToGetLunarInfo.mind + " \n" +
							 "Constitution = " + raceToGetLunarInfo.constitution + " " +
							 "Spirit = " + raceToGetLunarInfo.spirit;
		}
	}

}
