using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapDestinationSettings mapDestination;
    public QuestController questController;
    public Map[] defaultMaps;

    private void Start() {
        Quest.QuestInfo q = questController.GetCurrentInfo();
        Map mapBeingCreated = null;
        if(q != null && q.forcedMap){
            mapBeingCreated = Instantiate<Map>(q.forcedMap,Vector3.zero,Quaternion.identity);
        }else{
            mapBeingCreated = Instantiate<Map>(defaultMaps[Random.Range(0,defaultMaps.Length)],Vector3.zero,Quaternion.identity);
        }
        mapBeingCreated.InitializeMap(mapDestination);
    }
}
