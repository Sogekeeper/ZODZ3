using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContainer : MonoBehaviour
{
    /*
        Intenção da classe é colocar ela em objetos de interesse que carregam pools relacionadas
        com ele. Começa vazio e quando um objeto tenta acessar uma pool aqui que ainda não foi
        gerada antes, ele gera a pool e retorna o objeto. O ID das pools é baseado no nome da 
        prefab.

        Motivo inicial para criar isso foi a necessidade de ter pools para cada skill ranged que
        o jogador equipar porém sem criar pools para tudo. Jogador começa sem pool, e assim
        que equipar uma ranged, ele gera uma pool do que a skill precisar e sem usar singleton.

        Talvez nem todas as pools do jogo usarão isso.

        Para manter uma mesma pool para grupos maiores (evitar que cada inimigo crie uma pool ao
        aparecer), uma opção é criar o container naquilo que spawna os inimigos que vão usar a pool.
        
        Para motivos de teste e independencia de prefab, caso um inimigo tente acessar uma pool mas
        sua var de PoolContainer não foi setada por um spawner (arrastado manualmente pra cena), 
        basta checar se a var é nula e se for usa o AddComponent nele mesmo.
    */

    [System.Serializable]
    public class Pool{
        public string poolID; 
        public PoolObject[] objectPool;
        public int currentPoolIndex = 0;

        public Pool(PoolObject targetPrefab, int size, Transform targetParent = null){
            poolID = targetPrefab.name;
            objectPool = new PoolObject[size];
            for(int i = 0; i < objectPool.Length; i++){
                PoolObject po = Instantiate<PoolObject>(targetPrefab, new Vector3(-1000,0,0), Quaternion.identity,targetParent);
                objectPool[i] = po;
                objectPool[i].gameObject.SetActive(false);
            }
        }

        public PoolObject GetNextObject(){
            if(objectPool == null) return null;
            PoolObject po;
            po = objectPool[currentPoolIndex];
            currentPoolIndex = (currentPoolIndex + 1) % objectPool.Length;
            return po;
        }

        public void ClearPool(){
            for(int i = 0; i < objectPool.Length; i++){
                Destroy(objectPool[i].gameObject);
            }
            objectPool = null;
        }
    }

    public List<Pool> currentPools; //terá tamanho dinamico então lista

    private void Awake() {
        currentPools = new List<Pool>();
    }

    public Pool CreatePoolIfNone(PoolObject target, int amountToGenerateIfNone = 20, Transform targetParent = null){
        Pool targetPool = LookForPool(target.name);
        if(targetPool == null){
            targetPool = new Pool(target, amountToGenerateIfNone,targetParent);
            currentPools.Add(targetPool);
        }
        return targetPool;
    }

    public PoolObject SpawnTargetObject(PoolObject target, int amountToGenerateIfNone = 20, Transform targetParent = null){
        Pool targetPool = CreatePoolIfNone(target, amountToGenerateIfNone, targetParent);
        PoolObject po = targetPool.GetNextObject();
        if(targetParent) po.transform.SetParent(targetParent);
        po.gameObject.SetActive(true);
        po.OnSpawn?.Invoke();
        return po;
    }

    public void DestroyPoolIfExists(PoolObject target){
        Pool targetPool = LookForPool(target.name);
        if(target != null){
            targetPool.ClearPool();
            if(currentPools.Contains(targetPool)){
                currentPools.Remove(targetPool);
            }
        }
    }

    private Pool LookForPool(string targetID){        
        for(int i = 0; i < currentPools.Count;i++){
            if(currentPools[i].poolID.Equals(targetID)){
                return currentPools[i];
            }
        }

        return null;
    }
    
}
