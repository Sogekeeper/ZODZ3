using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : Aim
{
    public bool usingCursor = true;
    public Transform optionalRotatingPointer;

    private Vector3 mousePos;
    private Camera mainCam;
    [Header("Optional")]
    public Transform projectileSpawnPoint;

    private void Start() {
        mainCam = Camera.main;
        aimDirection = new Vector3(0,0,0);
    }

    private void Update() {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        focusPoint = mousePos;

        if(projectileSpawnPoint)
            aimDirection = mousePos - projectileSpawnPoint.position;
        else
            aimDirection = mousePos - transform.position;
        aimDirection = new Vector3(aimDirection.x, aimDirection.y, 0);

        if(optionalRotatingPointer){
            RotateObjectToAim(optionalRotatingPointer);
        }
    }

}
