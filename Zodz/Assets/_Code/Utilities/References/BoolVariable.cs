using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool Variable", menuName="Variables/Bool Variable")]
public class BoolVariable : ScriptableObject
{
    [SerializeField]
  private bool value = false;

  public bool Value
  {
    get { return value; }
    set { this.value = value; }
  }

  public void SetValueFunction(bool targetBool){
      Value = targetBool;
  }
}
