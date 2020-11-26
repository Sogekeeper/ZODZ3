using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour
{
    //para saber detalhes do inimigo que pode ser instanciado no mapa
    public MapPointer.PointSize entitySize;
    public int entityCost = 3;
    public int minimumTotalCost = 0;
}
