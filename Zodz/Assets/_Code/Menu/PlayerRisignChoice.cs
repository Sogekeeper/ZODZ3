using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRisignChoice : MonoBehaviour
{
	public static Race playerRisignChoice;
	public PlayerCharacterSettings playerCharacterSettings;

	public void ChooseRisingSign(UpdateRisingInfo risingSign)
	{
		playerRisignChoice = risingSign.raceToGetRisingInfo;
		playerCharacterSettings.ascendantRace = playerRisignChoice;
	}
}
