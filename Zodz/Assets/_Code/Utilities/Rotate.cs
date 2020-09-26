using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public int minSpeed = 0;
	public int maxSpeed = 0;
	[SerializeField]
	private int speed;
	// Start is called before the first frame update
	void Start()
    {
		speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
	}
}
