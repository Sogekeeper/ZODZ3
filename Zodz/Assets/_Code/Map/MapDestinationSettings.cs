using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDestinationSettings", menuName = "Maps/Destinations", order = 4)]
public class MapDestinationSettings : ScriptableObject
{
    public StringVariable pastScene;
    public StringVariable destinationScene;
}
