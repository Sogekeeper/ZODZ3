using UnityEngine;

public class WorldCanvasFix : MonoBehaviour
{
    private bool initialized = false;

    private void OnEnable() {
        //caso um canvas em world space deixa o campo world camera vazio, unity chama
        //FindGameObjectWithTag todo frame.
        if(initialized) return;
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        initialized = true;
    }
}
