using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRaceInMenuController : MonoBehaviour
{
	public UpdateRaceChoosed[] charactersPanels;
	public CharacterSelectButtonFunction[] charactersPanelsSelect;
	public bool canDraftEqualsRaces;
	private Race choice1;
	private Race choice2;
	public RacesAvaliableToChoice racesScript;

	private void Awake()
	{
		if (canDraftEqualsRaces)
		{
			for (int i = 0; i < charactersPanels.Length; i++)
			{
				int index = Random.Range(0, racesScript.solarRaces.Length);
				Race random = racesScript.solarRaces[index];
				charactersPanels[i].raceDefault = random;
				charactersPanelsSelect[i].race = random;

			}
		}
		else
		{
			int index = Random.Range(0, racesScript.solarRaces.Length);
			Race random = racesScript.solarRaces[index];
			charactersPanels[0].raceDefault = random;
			charactersPanelsSelect[0].race = random;

			int index2 = Random.Range(0, racesScript.solarRaces.Length);
			Race random2 = racesScript.solarRaces[index2];

			for (int i = 0; i < racesScript.solarRaces.Length; i++)
			{
				if (random2 == racesScript.solarRaces[index])
				{
					index2 = (index2 + 1) % racesScript.solarRaces.Length;
					random2 = racesScript.solarRaces[index2];
				}
				else
				{			
					break;
				}

			}
			charactersPanels[1].raceDefault = random2;
			charactersPanelsSelect[1].race = random2;

			int index3 = Random.Range(0, racesScript.solarRaces.Length);
			Race random3 = racesScript.solarRaces[index3];

			for (int i = 0; i < racesScript.solarRaces.Length; i++)
			{
				if (random3 == racesScript.solarRaces[index] || random3 == racesScript.solarRaces[index2])
				{
					index3 = (index3 + 1) % racesScript.solarRaces.Length;
					random3 = racesScript.solarRaces[index3];
				}
				else
				{
					break;
				}
			}
			charactersPanels[2].raceDefault = random3;
			charactersPanelsSelect[2].race = random3;

		}
	}
}
