using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateFinalCanvas : MonoBehaviour
{
	public Image lunarIcon;
	public Image risingIcon;
	public Sprite leoIcon;
	public Sprite piscesIcon;
	public Sprite virgoIcon;


	private void OnEnable()
	{
		risingIcon.sprite = PlayerRisignChoice.playerRisignChoice.raceIcon;
		lunarIcon.sprite = PlayerLunarChoice.playerLunarChoice.raceIcon;
	}

}
