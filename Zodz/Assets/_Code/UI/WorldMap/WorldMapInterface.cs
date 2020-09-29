using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WorldMapInterface : MonoBehaviour
{
    public WorldSettings worldObject;
    public MapSettings globalMapSettings;
    public StringVariable roomSceneString;
    public Button travelButton;
    public GameObject infoPanel;
    [Header("Info Panel Details")]
    public Text locationNameText;
    public Text locationDescriptionText;
    public Image locationImage;
    public float imageTileWidth = 33;
    public RectTransform difficultyIndicator;
    public RectTransform distanceIndicator;
    public GameObject youreHereContent;
    public Text youreHereText;
    public GameObject pathwayInfoContent;

    public static bool isOpen = false;
    private Location selectedLocation;
    private WorldSettings.Pathway selectedPathway;

    [Header("Debug")]
    public Location fakeOrigin;

    private void Start() {
        //gameObject.SetActive(false);
        SelectLocation(null);
    }

    public void ToggleMap(InputAction.CallbackContext context){
        if(context.performed){
            if(isOpen)CloseMap();
            else OpenMap(false);
        }
    }

    public void OpenMap(bool activeTravelButton){
        gameObject.SetActive(true);
        isOpen = true;
        Time.timeScale = 0;
        travelButton.gameObject.SetActive(activeTravelButton);
        SelectLocation(null);
    }

    public void CloseMap(){
        if(!PauseMenu.isPaused) Time.timeScale = 1;
        isOpen = false;
        SelectLocation(null);
        travelButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Travel(){
        if(!selectedLocation || !worldObject.currentLocation || selectedPathway == null) return;
        globalMapSettings.destinationScene = selectedLocation.locationSceneString;
        globalMapSettings.startRoomSet = worldObject.currentLocation.roomSet;
        globalMapSettings.startEnemySet = worldObject.currentLocation.enemySet;
        globalMapSettings.endRoomSet = selectedLocation.roomSet;
        globalMapSettings.endEnemySet = selectedLocation.enemySet;
        globalMapSettings.numberOfRooms = selectedPathway.distance;
        globalMapSettings.mapDifficulty = MapSettings.MapDifficulty.EASY;
        Time.timeScale = 1;
        worldObject.SetPathway(selectedPathway);
        SceneManager.LoadScene(roomSceneString.Value);        
    }

    public void SelectLocation(Location target){
        if(target == null){
            selectedLocation = null;
            travelButton.interactable = false;
            infoPanel.SetActive(false);
            return;
        }        
        infoPanel.SetActive(true);
        locationNameText.text = target.locationName;
        locationDescriptionText.text = target.locationDescription;
        locationImage.sprite = target.locationIcon;
        if(worldObject.currentLocation != null && worldObject.currentLocation == target){
            pathwayInfoContent.SetActive(false);
            youreHereContent.SetActive(true);
            youreHereText.text = "YOU ARE\nHERE";
        }else if(worldObject.currentLocation != null){
            WorldSettings.Pathway path = worldObject.GetPathway(worldObject.currentLocation,target);
            if(path == null){
                selectedLocation = null;
                pathwayInfoContent.SetActive(false);
                youreHereContent.SetActive(true);
                youreHereText.text = "NOT CLOSE\nENOUGH";
                return;
            }
            pathwayInfoContent.SetActive(true);
            youreHereContent.SetActive(false);
            selectedLocation = target;
            selectedPathway = path;
            travelButton.interactable = true;
            float diff = Mathf.Clamp(Mathf.Round((path.difficulty/worldObject.maxDifficultyFactor)*3),1,3);
            float dist = Mathf.Clamp(Mathf.Round((path.distance/worldObject.maxRoomAmount)*3),1,3);
            Debug.Log("Diff: "+diff+" / Dist: "+dist);
            difficultyIndicator.LeanSize(new Vector2(diff*imageTileWidth,difficultyIndicator.rect.height),0.3f).setIgnoreTimeScale(true);
            distanceIndicator.LeanSize(new Vector2(dist*imageTileWidth,difficultyIndicator.rect.height),0.3f).setIgnoreTimeScale(true);
        }else if(worldObject.currentLocation == null){
            //erro
            selectedLocation = null;
            pathwayInfoContent.SetActive(false);
            youreHereContent.SetActive(true);
            youreHereText.text = "????";
            return;
        }
    }

    [ContextMenu("DEBUG - Init World Object")]
    public void DebugInitWorld(){
        worldObject.InitializeWorld(fakeOrigin);
    }
}

