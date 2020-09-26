using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnalysis 
{
    public static Transform GetNextTransformReference(Transform previousReference, TransformRuntimeSet transformSet){
        int randomIndex = (int)Random.Range(0,transformSet.Items.Count);
        for(int i = 0; i < transformSet.Items.Count; i++){
            if(!previousReference || previousReference != transformSet.Items[randomIndex]){
                previousReference = transformSet.Items[randomIndex];
                return previousReference;
            }
            else{
                randomIndex = (randomIndex + 1) % transformSet.Items.Count;
            }
        }
        return null;
    }

    public static Transform GetNextClosestTransformReference(Transform previousReference, TransformRuntimeSet transformSet, Transform entity){
        int randomIndex = (int)Random.Range(0,transformSet.Items.Count);
        float distToRef = Mathf.Infinity;
        for(int i = 0; i < transformSet.Items.Count; i++){
            float currDist = Vector3.Distance(entity.position,transformSet.Items[randomIndex].position);
            if(!previousReference || previousReference != transformSet.Items[randomIndex]){
                if(currDist < distToRef){
                    previousReference = transformSet.Items[randomIndex];
                    distToRef = currDist;
                    return previousReference;                    
                }else{
                    randomIndex = (randomIndex + 1) % transformSet.Items.Count;    
                }
            }
            else{
                randomIndex = (randomIndex + 1) % transformSet.Items.Count;
            }
        }
        return null;
    }

    public static Transform GetClosestEnemy(List<EntityRuntimeSet> targetEntities, Transform origin, bool ignoreWalls = true, int layerValue =-1){
        Transform result = null;
        float currentDistance = Mathf.Infinity;
        for(int i = 0; i < targetEntities.Count;i++){
            if(targetEntities[i].Items == null) continue;
            for(int y = 0; y < targetEntities[i].Items.Count; y++){
                float dist = Vector2.Distance(origin.position, targetEntities[i].Items[y].transform.position);
                if(!ignoreWalls && layerValue >= 0){
                    RaycastHit2D hit = Physics2D.Linecast(origin.position,targetEntities[i].Items[y].transform.position,layerValue);                   
                    if(hit){
                        Debug.DrawLine(origin.position,targetEntities[i].Items[y].transform.position,Color.red);
                        continue;
                    }else
                        Debug.DrawLine(origin.position,targetEntities[i].Items[y].transform.position,Color.blue);
                }
                if(dist<currentDistance){
                    result = targetEntities[i].Items[y].transform;  
                    currentDistance = dist;
                }
            }
        }
        return result;
    }
}
