using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Vector3 aimDirection{get;protected set;}    
    public Vector3 focusPoint;

    public void SetAimDirection(Vector2 targetDirection){
        aimDirection = new Vector3(targetDirection.x,targetDirection.y,0);
        aimDirection.Normalize();
    }

    public void RotateObjectToAim(Transform objectToRotate){
        if(aimDirection == null){
            //Debug.Log("Aim Direction on "+gameObject.name+" is null. Can't rotate "+objectToRotate.name);
            return;
        }
        float angle = Mathf.Atan2(aimDirection.y,aimDirection.x) * Mathf.Rad2Deg - 90;
        objectToRotate.rotation = Quaternion.AngleAxis(angle,Vector3.forward);        
    }

    public GeneralDirection GetGeneralDirection(){
        if(Mathf.Abs(aimDirection.y) > Mathf.Abs(aimDirection.x)){//cima ou baixo
            if(aimDirection.y > 0){
                return GeneralDirection.UP;
            }else{
                return GeneralDirection.DOWN;
            }
        }else{//direita ou esquerda
            if(aimDirection.x > 0){
                return GeneralDirection.RIGHT;
            }else{
                return GeneralDirection.LEFT;
            }
        }
    }

    public float GetDistanceBetweenFocus(){
        if(focusPoint == null) return 0;
        return Vector3.Distance(transform.position,focusPoint);
    }

}

public enum GeneralDirection{
    UP, DOWN, LEFT, RIGHT
}