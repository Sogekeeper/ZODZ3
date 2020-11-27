using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapItem : MonoBehaviour
{
    public Location location;
    public WorldMapInterface mapInterface;

    bool isOpen = false;
    bool hover = false;
    Animator anim;
    static Location selectedLocation;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start(){
        if(mapInterface.worldObject.currentLocation == location){
            mapInterface.currentLocationIndicator.position = transform.position + new Vector3(0,0.6f,0);
            anim.SetTrigger("open");
            isOpen = true;
        }
    }

    private void OnMouseDown() {
        mapInterface.SelectLocation(location);
    }

    public void SelectionWasUpdated(Location selected){
        selectedLocation = selected;
        if(mapInterface.worldObject.currentLocation == location || hover) return;
        if(selected != location && isOpen){
            Close();
        }
    }


    private void OnMouseEnter(){
        hover = true;
        if(isOpen) return;
        Open();
    }
    private void OnMouseExit(){
        hover = false;
        if(mapInterface.worldObject.currentLocation == location || selectedLocation == location) return;
        Close();
    }

    private void Open(){
        anim.SetTrigger("open");
        isOpen = true;
    }

    private void Close(){
        anim.SetTrigger("close");
        isOpen = false;
    }
}