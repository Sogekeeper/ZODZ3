using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRisingCanvas : MonoBehaviour
{
	public Image lunarIcon;

	private void OnEnable()
	{
			lunarIcon.sprite = PlayerLunarChoice.playerLunarChoice.raceIcon;
	}

}
