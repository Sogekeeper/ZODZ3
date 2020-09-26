using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	private void Awake()
	{
	
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
		{
			SetTimeScaleTo0(true);
			transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	public void SetTimeScaleTo0(bool trueSet0)
	{
		if(trueSet0)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
