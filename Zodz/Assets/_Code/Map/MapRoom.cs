using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_Room", menuName = "Map/Map Room", order = 1)]
public class MapRoom : ScriptableObject
{
    public GameObject roomBase; //ground floor
    public MapDecoration[] decorations;

    private int previousIndex = -1;

    public MapDecoration GetRandomDecoration(){
        int index = Random.Range(0,decorations.Length);
        if(index == previousIndex){
            index = (index + 1) % decorations.Length;
        }
        return decorations[index];
    }
}
