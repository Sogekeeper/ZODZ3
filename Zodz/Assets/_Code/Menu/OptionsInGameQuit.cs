using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsInGameQuit : MonoBehaviour
{
	public void QuitToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void QuitToDesktop()
	{
		Application.Quit();
	}
}
