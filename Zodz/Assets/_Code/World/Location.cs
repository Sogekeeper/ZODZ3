using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "World/Location", order = 0)]
public class Location : ScriptableObject
{
    public string locationName;
    public StringVariable locationSceneString;
    public int distanceFactor;
    public Sprite locationIcon;
    public MapRoomSet roomSet;
    public MapEnemySet enemySet;
    public Location[] neighbors;
    [TextArea]public string locationDescription;
}
