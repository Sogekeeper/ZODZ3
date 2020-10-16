using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float parallaxEffect;
    private Vector3 spriteSize, startPos;

    private void Start() {
        startPos = transform.position;
        spriteSize = GetComponent<SpriteRenderer>().bounds.size;
    }

    private void LateUpdate() {
        Vector3 temp = (cam.position *(1 - parallaxEffect));
        temp.z = 0;
        Vector3 dist = (cam.position * parallaxEffect);
        dist.z = 0;

        transform.position = startPos + dist;
        if(temp.y > startPos.y + spriteSize.y) startPos.y += spriteSize.y;
        else if(temp.y < startPos.y - spriteSize.y) startPos.y -= spriteSize.y;
        if(temp.x > startPos.x + spriteSize.x) startPos.x += spriteSize.x;
        else if(temp.x < startPos.x - spriteSize.x) startPos.x -= spriteSize.x;
    }
}
