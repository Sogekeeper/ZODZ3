using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeLunar : MonoBehaviour
{
	public UpdateLunarInfo[] lunarPanels;
	public bool canDraftEqualsRaces;
	private Race choice1;
	private Race choice2;
	public RacesAvaliableToChoice racesScript;

	private void Awake()
	{
		if (canDraftEqualsRaces)
		{
			for (int i = 0; i < lunarPanels.Length; i++)
			{
				int index = Random.Range(0, racesScript.lunarAndRisingRaces.Length);
				Race random = racesScript.lunarAndRisingRaces[index];
				lunarPanels[i].raceToGetLunarInfo = random;

			}
		}
		else
		{
			int index = Random.Range(0, racesScript.lunarAndRisingRaces.Length);
			Race random = racesScript.lunarAndRisingRaces[index];
			lunarPanels[0].raceToGetLunarInfo = random;

			int index2 = Random.Range(0, racesScript.lunarAndRisingRaces.Length);
			Race random2 = racesScript.lunarAndRisingRaces[index2];

			for (int i = 0; i < racesScript.lunarAndRisingRaces.Length; i++)
			{
				if (random2 == racesScript.lunarAndRisingRaces[index])
				{
					index2 = (index2 + 1) % racesScript.lunarAndRisingRaces.Length;
					random2 = racesScript.lunarAndRisingRaces[index2];
				}
				else
				{
					break;
				}

			}
			lunarPanels[1].raceToGetLunarInfo = random2;

			int index3 = Random.Range(0, racesScript.lunarAndRisingRaces.Length);
			Race random3 = racesScript.lunarAndRisingRaces[index3];

			for (int i = 0; i < racesScript.lunarAndRisingRaces.Length; i++)
			{
				if (random3 == racesScript.lunarAndRisingRaces[index] || random3 == racesScript.lunarAndRisingRaces[index2])
				{
					index3 = (index3 + 1) % racesScript.lunarAndRisingRaces.Length;
					random3 = racesScript.lunarAndRisingRaces[index3];
				}
				else
				{
					break;
				}
			}
			lunarPanels[2].raceToGetLunarInfo = random3;
			Debug.Log("Randomizando Lunar");
		}
	}
}
