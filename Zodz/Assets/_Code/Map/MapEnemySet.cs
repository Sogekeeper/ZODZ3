using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_Enemy_Set", menuName = "Map/Map Enemy Sets", order = 3)]
public class MapEnemySet : ScriptableObject
{
  public MapEntity[] entities;

  public MapEntity GetRandomEntity(MapPointer.PointSize targetSize,int availablePoints,int totalPoints){
    int randomIndex = (int)Random.Range(0,entities.Length);
    for(int i = 0; i < entities.Length; i++){
      //analisar custo e tamanho desejado
      if((int)entities[randomIndex].entitySize <= (int)targetSize 
      && entities[randomIndex].entityCost <= availablePoints
      && entities[randomIndex].minimumTotalCost <= totalPoints){
        return entities[randomIndex];
      }
      randomIndex = (randomIndex + 1) % entities.Length;
    }
    //Debug.Log("No compatible entity found");
    return null;
  }
}
