using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDecoration : MonoBehaviour
{
    public Transform playerSpawn;
    public MapDoor[] doors;
    public MapPointer[] pointers;

    public void ClearPointers(){
        for(int i = 0; i < pointers.Length; i++){
            pointers[i].used = false;
        }
    }

    public MapPointer GetRandomAvailablePointer(){
        MapPointer result = null;
        int randomPointIndex = (int)Random.Range(0,pointers.Length);
        for(int i = 0; i < pointers.Length; i++){            
            if(!pointers[randomPointIndex].used){
                result = pointers[randomPointIndex];
                break;
            }
            randomPointIndex = (randomPointIndex + 1) % pointers.Length;
        }

        return result;
    }
}
