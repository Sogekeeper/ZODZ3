using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Element")]
public class Element : ScriptableObject
{
    public string elementName = "water";
    public Sprite elementIcon;
    public Element[] strongAgainst;
}
