using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPortal : MonoBehaviour
{
    public WorldMapInterface mapInterface;

    public void UsePortal(){
        mapInterface.OpenMap(true);
    }

}
