using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToTransformSet : MonoBehaviour
{
    public TransformRuntimeSet targetSet;

    private void OnEnable() {
        if(targetSet){
            targetSet.Add(transform);
        }
    }

    private void OnDisable() {
        if(targetSet){
            targetSet.Add(transform);
        }
    }
}
