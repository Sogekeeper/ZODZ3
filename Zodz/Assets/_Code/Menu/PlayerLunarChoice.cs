using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLunarChoice : MonoBehaviour
{
	public static Race playerLunarChoice;
	public PlayerCharacterSettings playerCharacterSettings;

	public void ChooseMoonSign(Race lunarSign)
	{

		playerLunarChoice = lunarSign;				
		playerCharacterSettings.lunarRace = playerLunarChoice;
		
	}
}
