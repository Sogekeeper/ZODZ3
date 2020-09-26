using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTo : MonoBehaviour
{
	public bool ignoreTimeScale;
	private void OnEnable()
	{
		this.GetComponent<RectTransform>().localScale = Vector3.zero;
		if(ignoreTimeScale)
			LeanTween.scale(this.gameObject, Vector3.one, 0.5f).setEaseOutBack().setIgnoreTimeScale(true);
		else
			LeanTween.scale(this.gameObject, Vector3.one, 0.5f).setEaseOutBack();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
