using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapItem : MonoBehaviour
{
    public Location location;
    public WorldMapInterface mapInterface;

    private void OnMouseDown() {
        mapInterface.SelectLocation(location);
    }
}
