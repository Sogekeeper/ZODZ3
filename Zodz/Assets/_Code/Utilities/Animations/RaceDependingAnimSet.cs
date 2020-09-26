using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Race_Animation_Set", menuName = "Animations/Race Anim Set", order = 2)]
public class RaceDependingAnimSet : ScriptableObject
{
  [System.Serializable]
  public class SetInfo
  {
    public Race setOwner;
    public AnimationSet targetSet;
  }

  public SetInfo[] possibleSets;

  public AnimationSet GetSetForRace(Race targetRace)
  {
    AnimationSet resultSet = null;
    if (possibleSets != null && possibleSets.Length > 0)
    {
      for (int i = 0; i < possibleSets.Length; i++)
      {
        if (possibleSets[i].setOwner == targetRace)
        {
          resultSet = possibleSets[i].targetSet;
        }
      }
    }
    if (resultSet == null)
    {
      Debug.LogError("Busca por Clip sem resultado");
      return resultSet;
    }
    return resultSet;
  }
}
