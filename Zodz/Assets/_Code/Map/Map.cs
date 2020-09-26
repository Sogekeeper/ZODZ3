using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //OLD, NOT USED
    //Now using MapRoomGenerator and MapRoom



    public MapPortal destinationPortal;
    public MapPortal originPortal;

    public void InitializeMap(MapDestinationSettings mapSettings){
        //destinationPortal.destinationScene = mapSettings.destinationScene;
        //originPortal.destinationScene = mapSettings.pastScene;
        originPortal.gameObject.SetActive(false);
        destinationPortal.gameObject.SetActive(true);
    }
}
