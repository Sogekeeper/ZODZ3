using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
	public QuestController questController;
	private string startSceneName;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetConfigToStartGame()
	{
		questController.activeQuest = PlayerRaceChoice.playerRaceChoice.startigQuest;
		startSceneName = PlayerRaceChoice.playerRaceChoice.startingScene;
	}

	public void StartGame()
	{
		SceneManager.LoadScene(startSceneName);
	}
}
