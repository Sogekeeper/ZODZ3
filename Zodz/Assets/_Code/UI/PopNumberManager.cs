using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopNumberManager : MonoBehaviour
{
    public PopNumber popPrefab;
    public int popPoolAmount = 2;

    private PopNumber[] popPool;
    private int currentPoolIndex = 0;

    private void Start() {
        popPool = new PopNumber[popPoolAmount];
        for(int i = 0; i < popPoolAmount; i++){
            PopNumber p = Instantiate<PopNumber>(popPrefab,transform.position,Quaternion.identity);
            p.gameObject.SetActive(false);
            popPool[i] = p;
        }
    }

    public void SpawnPopNumber(int valueToDisplay){
        PopNumber p = GetNextPoolItem();
        p.gameObject.SetActive(true);
        p.transform.position = transform.position;
        p.InitPopNumber(valueToDisplay);
    }

    private PopNumber GetNextPoolItem(){
        currentPoolIndex = (currentPoolIndex + 1) % popPool.Length;
        return popPool[currentPoolIndex];
    }
}
