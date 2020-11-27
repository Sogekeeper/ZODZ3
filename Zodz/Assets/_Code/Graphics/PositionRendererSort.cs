using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSort : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 5000;
    [SerializeField]
    private float offset = 0;
    [SerializeField]
    private bool runOnlyOnce = true;

    private float timer;
    private float timerMax = 0.3f;
    private Renderer myRenderer;

    private void Awake() {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate() {
        timer -= Time.deltaTime;
        if(timer <= 0){
            timer = timerMax;
            ChangeSortOrder();
            if(runOnlyOnce){
                Destroy(this);
            }
        }
    }

    [ContextMenu("Change Sort Order")]
    public void ChangeSortOrder(){
        if(myRenderer == null)myRenderer = gameObject.GetComponent<Renderer>();
        myRenderer.sortingOrder = (int)(sortingOrderBase-transform.position.y - offset);
    }
}
