using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_Room_Set", menuName = "Map/Map Room Set", order = 2)]
public class MapRoomSet : ScriptableObject
{
    public MapRoom[] possibleRooms;

    private int previousIndex = -1;

    public MapRoom GetRandomRoom(){
        int index = Random.Range(0,possibleRooms.Length);
        if(index == previousIndex){
            index = (index + 1) % possibleRooms.Length;
        }
        return possibleRooms[index];

    }
}
