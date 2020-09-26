using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker),typeof(Rigidbody2D))]
public class AIChaseBehaviour : MonoBehaviour
{
    public Transform target; //deixar isso null para parar de andar

    public float speed = 3f;
    public float nextWaypointDistance = 2.5f;

    public Vector2 currentMovDirection{get; private set;}
    public bool reachedEndOfPath{get; private set;} 

    private Path path;
    private int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    private void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        reachedEndOfPath = false;
        currentMovDirection = new Vector2(0,0);

        StartCoroutine(UpdatePathRoutine());
    }

    public IEnumerator UpdatePathRoutine(){
        while(true){
            if(seeker.IsDone() && target
                && Vector2.Distance(rb.position,target.position) > nextWaypointDistance){
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    public float GetDistanceToTarget(){
        if(target){
            return Vector2.Distance(rb.position, target.position);
        }else{
            return 0;
        }
    }

    private void FixedUpdate() {
        if(path == null)
            return;
        
        if(currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            return;
        }else{
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 movement = direction * speed * Time.fixedDeltaTime;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance){
            currentWaypoint++;
        }
        else
            currentMovDirection = direction;

        rb.AddForce(movement);
        /* if(!reachedEndOfPath && target)
            rb.MovePosition(rb.position + movement);
        else
            rb.MovePosition(rb.position); */
    }

    public void Stop(){
        target = null;
        rb.velocity = Vector3.zero;
    }
}
