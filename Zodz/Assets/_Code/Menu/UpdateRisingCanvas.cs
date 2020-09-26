using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRisingCanvas : MonoBehaviour
{
	public Image lunarIcon;
	public Sprite leoIcon;
	public Sprite piscesIcon;
	public Sprite virgoIcon;

	private void OnEnable()
	{
		if(PlayerLunarChoice.playerLunarChoice.name == "Leo")
		{
			lunarIcon.sprite = leoIcon;
		}
		else if (PlayerLunarChoice.playerLunarChoice.name == "Pisces")
		{
			lunarIcon.sprite = piscesIcon;
		}
		else if (PlayerLunarChoice.playerLunarChoice.name == "Virgo")
		{
			lunarIcon.sprite = virgoIcon;
		}
	}

}
