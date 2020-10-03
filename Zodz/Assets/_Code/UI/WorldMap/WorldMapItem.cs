using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapItem : MonoBehaviour
{
    public Location location;
    public WorldMapInterface mapInterface;

    private void Start(){
        if(mapInterface.worldObject.currentLocation == location){
            mapInterface.currentLocationIndicator.position = transform.position + new Vector3(0,0.6f,0);
        }
    }

    private void OnMouseDown() {
        mapInterface.SelectLocation(location);
    }
}
