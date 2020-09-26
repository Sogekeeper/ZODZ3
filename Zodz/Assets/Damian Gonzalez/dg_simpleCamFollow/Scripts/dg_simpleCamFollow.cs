//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class dg_simpleCamFollow : MonoBehaviour
{
    public Transform target;
    [Range(1f,40f)] public float laziness = 10f;
    public bool lookAtTarget = true;
    public bool takeOffsetFromInitialPos = true;
    public Vector3 generalOffset;
    Vector3 whereCameraShouldBe;
    bool warningAlreadyShown = false;
    
    [Space()]
    public bool useConstraints = false;
    public Vector2 horizontalConstraints;
    public Vector2 verticalConstraints;

    private void Start() {
        if (takeOffsetFromInitialPos && target != null) generalOffset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target != null) {
            whereCameraShouldBe = target.position + generalOffset;
            
            if(useConstraints){
                if(whereCameraShouldBe.x < horizontalConstraints.x)
                    whereCameraShouldBe.x = horizontalConstraints.x;
                else if(whereCameraShouldBe.x > horizontalConstraints.y)
                    whereCameraShouldBe.x = horizontalConstraints.y;

                if(whereCameraShouldBe.y < verticalConstraints.x)
                    whereCameraShouldBe.y = verticalConstraints.x;
                else if(whereCameraShouldBe.y > verticalConstraints.y)
                    whereCameraShouldBe.y = verticalConstraints.y;
            }
            

            transform.position = Vector3.Lerp(transform.position, whereCameraShouldBe, 1 / laziness);

            if (lookAtTarget) transform.LookAt(target);
        } else {
            if (!warningAlreadyShown) {
                Debug.Log("Warning: You should specify a target in the simpleCamFollow script.", gameObject);
                warningAlreadyShown = true;
            }
        }
    }
}
