using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstPrefabSpawn : MonoBehaviour
{
    public PoolContainer pooler;
    public PoolObject targetPrefab;
    public int amountToPool = 10;
    public Vector2 amountConstraints;
    public Vector2 forceConstraints;

    [ContextMenu("DEBUG - Manual Spawn Prefabs")]
    public void SpawnPrefabs(){
        int targetAmount =  Mathf.RoundToInt(Random.Range(amountConstraints.x,amountConstraints.y));
        for(int i = 0; i < targetAmount; i++){
            PoolObject po = pooler.SpawnTargetObject(targetPrefab,amountToPool,transform);
            po.transform.position = transform.position;
            float targetForce = Random.Range(forceConstraints.x,forceConstraints.y);
            Rigidbody2D rb = po.GetComponent<Rigidbody2D>();
            if(rb){
                rb.AddForce(targetForce*Random.insideUnitCircle,ForceMode2D.Impulse);
            }
        }
    }
}
