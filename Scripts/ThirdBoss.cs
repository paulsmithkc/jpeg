using UnityEngine;
using System.Collections;

public class ThirdBoss : MonoBehaviour {
	public Transform target;
	public float speed = 4;

	public bool isCharging = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isCharging) 
		{
			rigidbody.MovePosition (transform.position + (transform.forward * speed * Time.deltaTime));
		} else {
			transform.LookAt(target);
		}
	}

	void StartCharge()
	{
		isCharging = true;
	}

	void StopCharging()
	{
		isCharging = false;
	}
}
