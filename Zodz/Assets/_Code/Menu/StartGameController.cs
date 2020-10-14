using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
	public QuestController questController;
	public WorldSettings worldSettings;
	private string startSceneName;
	
	private void Awake() {
		
	}

	public void SetConfigToStartGame()
	{
		questController.activeQuest = PlayerRaceChoice.playerRaceChoice.startigQuest;
		startSceneName = PlayerRaceChoice.playerRaceChoice.startingScene;
	}

	public void StartGame()
	{
		worldSettings.InitializeWorld(PlayerRaceChoice.playerRaceChoice.startingLocation);
		SceneManager.LoadScene(startSceneName);
	}
}
