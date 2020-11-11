using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        EntityStats es = other.GetComponent<EntityStats>();
        if(es) es.Fall();
    }
}
