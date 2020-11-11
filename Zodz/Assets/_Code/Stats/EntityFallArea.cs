using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EntityFallArea : MonoBehaviour
{
    public EntityStats entityStats;

    private BoxCollider2D boxCol;
    private Rigidbody2D rb;

    private void Start() {
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Hole") && !entityStats.falling){
            BoxCollider2D otherCol = other.GetComponent<BoxCollider2D>();
            if(otherCol.bounds.Contains(boxCol.bounds.max) && otherCol.bounds.Contains(boxCol.bounds.max)){
                entityStats.Fall();
            }
        }
    }
}
