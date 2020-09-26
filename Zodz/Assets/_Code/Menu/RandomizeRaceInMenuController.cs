using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRaceInMenuController : MonoBehaviour
{
	public Race[] races;
	public UpdateRaceChoosed[] charactersPanels;
	public bool canDraftEqualsRaces;
	private Race choice1;
	private Race choice2;

	private void Awake()
	{
		if (canDraftEqualsRaces)
		{
			for (int i = 0; i < charactersPanels.Length; i++)
			{
				int index = Random.Range(0, races.Length);
				Race random = races[index];
				charactersPanels[i].raceDefault = random;
			}
		}
		else
		{
			int index = Random.Range(0, races.Length);
			Race random = races[index];
			charactersPanels[0].raceDefault = random;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
