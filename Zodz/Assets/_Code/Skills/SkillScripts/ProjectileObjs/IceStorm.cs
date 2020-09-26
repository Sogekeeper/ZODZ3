using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStorm : ProjectileObject
{
    public PoolContainer pool;
    public PoolObject iceShardPrefab;
    public float angleBetweenShards = 22.5f;

    public override void InitializeProjectile(SkillUser user)
    {
        float currentAngle = 0;
        while(currentAngle < 360){
            PoolObject p = pool.SpawnTargetObject(iceShardPrefab, 20);
            p.GetComponent<ProjectileObject>().InitializeProjectile(user);
            p.transform.position = transform.position;
            p.transform.rotation = Quaternion.Euler(0,0,currentAngle);
            currentAngle += angleBetweenShards;
        }
    }
}
