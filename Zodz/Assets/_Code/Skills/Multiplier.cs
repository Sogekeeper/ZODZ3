using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Multiplier", menuName = "Skills/Augments/Multiplier", order = 0)]
public class Multiplier : ScriptableObject
{
    [SerializeField]
    private float[] possibleValues;
    [SerializeField]
    private int currentAmount = 0;

    public float GetValue(){
        return possibleValues[currentAmount];
    }
    public void ChangeValue(int toAdd){
        currentAmount += toAdd;
        currentAmount = Mathf.Clamp(currentAmount,0,possibleValues.Length-1);
    }

    public void Reset(){
        currentAmount = 0;
    }
}
