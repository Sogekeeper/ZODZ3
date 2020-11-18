using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulas
{
    public static int CalculateLifePoints(int constitutionValue){
        return (int)Mathf.Round(Mathf.Log(constitutionValue,4) * 500);
    }

    public static int CalculateManaPoints(int spiritValue){
        return (int)Mathf.Round(Mathf.Log(spiritValue,10) * 200);
    }
}
