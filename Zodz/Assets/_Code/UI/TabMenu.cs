using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    public GameObject[] tabContents;

    public void SelectContent(GameObject target){
        if(tabContents == null || target == null) return;
        for(int i = 0; i < tabContents.Length; i++){
            if(target == tabContents[i]){
                tabContents[i].SetActive(true);
            }else{
                tabContents[i].SetActive(false);
            }
        }

    }
}
