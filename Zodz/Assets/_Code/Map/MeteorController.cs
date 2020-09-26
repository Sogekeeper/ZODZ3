using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
	public Sprite[] meteoros;
	public float minSpeed;
	public float maxSpeed;
	private float speed = 0f;
	public int minAngle;
	public int maxAngle;
	private int angle;
	private Vector3 initialPos;
	public int timeInSecondsToRestart;
	private float actualTime = 0;

	private void Awake()
	{
		initialPos = transform.position;
	}

	// Start is called before the first frame update
	void Start()
    {
		randomizeMeteor();
		randomizeAngle();
		randomizeSpeed();
	}

    // Update is called once per frame
    void Update()
    {
		actualTime += Time.deltaTime;
		if(actualTime > timeInSecondsToRestart)
		{
			actualTime = 0;
			transform.position = initialPos;
			randomizeMeteor();
			randomizeAngle();
			randomizeSpeed();
		}
		this.transform.Translate(transform.right * -1 * speed * Time.deltaTime);
	}

	public void randomizeMeteor()
	{
		int choice = Random.Range(0, meteoros.Length);
		this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = meteoros[choice];
	}

	public void randomizeAngle()
	{ 
		angle = Random.Range(minAngle, maxAngle) + (int)transform.rotation.eulerAngles.z;
		gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	public void randomizeSpeed()
	{
		speed = Random.Range(minSpeed, maxSpeed);
	}

}
