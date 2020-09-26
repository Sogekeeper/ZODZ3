using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Entity Runtime Set", menuName="RuntimeSets/Entity Runtime Set")]
public class EntityRuntimeSet : RuntimeSet<EntityStats>
{
  public static bool SetArrayContainSet(List<EntityRuntimeSet> targetArray, EntityRuntimeSet targetSet){
    if(targetArray == null) return false;
    for(int i = 0; i < targetArray.Count; i++){
      if(targetArray[i] == targetSet){
        return true;
      }
    }
    return false;
  }

  public static bool DetectArrayOverlap(List<EntityRuntimeSet> array1, List<EntityRuntimeSet> array2){
    if(array1 == null || array2 == null) return false;
    for(int i = 0; i < array1.Count; i++){
      for(int y = 0; y < array2.Count; y++){
        if(array2[y] == array1[i]){
          return true;
        }
      }
    }
    return false;
  }

  public static List<EntityRuntimeSet> CopySetArray(List<EntityRuntimeSet> original){
    List<EntityRuntimeSet> result = new List<EntityRuntimeSet>();
    if(original == null) return null;
    for(int i = 0; i < original.Count; i++){
      result.Add(original[i]);
    }
    return result;
  }

  public static List<EntityRuntimeSet> CopySetArray(List<EntityRuntimeSet> original,ref List<EntityRuntimeSet> recipient,bool clearRecipient = true){
    if(original == null) return null;
    if(clearRecipient) recipient.Clear();
    for(int i = 0; i < original.Count; i++){
      if(clearRecipient) recipient.Add(original[i]);
      else{
        bool canAddSet = true;
        for(int y = 0; y < recipient.Count; y++){
          if(recipient[y] == original[i]) canAddSet = false;
        }
        if(canAddSet) recipient.Add(original[i]);
      }

    }
    return recipient;
  }

  public static List<EntityRuntimeSet> RemoveSetOverlap(List<EntityRuntimeSet> original,ref List<EntityRuntimeSet> recipient){
    if(original == null) return null;
    for(int i = 0; i < original.Count; i++){
      for(int y = 0; y < recipient.Count; y++){
        if(recipient[y] == original[i]) recipient.Remove(original[i]);
      }
    }
    return recipient;
  }
  
  
}
