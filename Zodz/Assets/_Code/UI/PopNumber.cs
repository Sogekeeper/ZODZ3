using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopNumber : MonoBehaviour
{
    //aqui futuramente inserir vars para identificar valores vermelhos/amarelos/laranjas

    public float yOffset = 0.4f;
    public float xOffsetRange = 0.35f;
    public float travelTime = 0.4f;
    public TextMeshProUGUI popText;

    private Vector3 targetPos;
    private Vector3 velor;

    private void Update() {
        BringTextUpSmoothly();
    }

    public void InitPopNumber(int number){
        popText.text = number.ToString();
        velor = Vector3.zero;
        transform.position = transform.position + new Vector3(Random.Range(-xOffsetRange,xOffsetRange),0,0);
        targetPos = transform.position + new Vector3(0,yOffset,0);
    }

    public void BringTextUpSmoothly(){
        if(gameObject.activeInHierarchy)
            transform.position = Vector3.SmoothDamp(transform.position,targetPos,ref velor,travelTime);
    }
}
