using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_Enemy_Set", menuName = "Map/Map Enemy Sets", order = 3)]
public class MapEnemySet : ScriptableObject
{
  public MapEntity[] easyEnemies;
  public MapEntity[] mediumEnemies;
  public MapEntity[] hardEnemies;

  public MapEntity GetRandomEntity(MapSettings.MapDifficulty targetDifficulty, MapPointer.PointSize targetSize,int availablePoints){
    MapEntity[] targetSet = null;
    if(targetDifficulty == MapSettings.MapDifficulty.EASY)
      targetSet = easyEnemies;
    if(targetDifficulty == MapSettings.MapDifficulty.MEDIUM)
      targetSet = mediumEnemies;
    if(targetDifficulty == MapSettings.MapDifficulty.HARD)
      targetSet = hardEnemies;
    int randomIndex = (int)Random.Range(0,targetSet.Length);
    for(int i = 0; i < targetSet.Length; i++){
      //analisar custo e tamanho desejado
      if((int)targetSet[randomIndex].entitySize <= (int)targetSize 
      && targetSet[randomIndex].entityCost <= availablePoints){
        return targetSet[randomIndex];
      }
      randomIndex = (randomIndex + 1) % targetSet.Length;
    }
    //Debug.Log("No compatible entity found");
    return null;
  }
}
