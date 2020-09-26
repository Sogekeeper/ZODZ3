using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRisignChoice : MonoBehaviour
{
	public static Race playerRisignChoice;
	public PlayerCharacterSettings playerCharacterSettings;

	public void ChooseRisingSign(Race risingSign)
	{
		playerRisignChoice = risingSign;
		playerCharacterSettings.ascendantRace = playerRisignChoice;
	}
}
