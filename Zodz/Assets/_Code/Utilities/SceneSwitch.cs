using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchToScene(int sceneIndex){
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }   

    public void SwitchToScene(string sceneName){
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }   
}
