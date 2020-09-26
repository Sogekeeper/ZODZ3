using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuObject;
    public static bool isPaused = false;

    private void Start() {
        menuObject.SetActive(false);
    }

    public void PauseGame(InputAction.CallbackContext context){
        if(!context.performed) return;
        if(menuObject.activeInHierarchy){
            PauseGame(false);
        }else{
            PauseGame(true);
        }
    }

    public void PauseGame(){
        if(menuObject.activeInHierarchy){
            PauseGame(false);
        }else{
            PauseGame(true);
        }
    }

    public void PauseGame(bool paused){
        if(!paused){
            Time.timeScale = 1;            
            menuObject.SetActive(false);
        }else{
            if(Time.timeScale == 0) return; //player is seeing another menu
            Time.timeScale = 0;
            menuObject.SetActive(true);
        }
        isPaused = paused;
    }

    public void TogglePauseMenu(bool active){
        menuObject.SetActive(active);
    }
}
