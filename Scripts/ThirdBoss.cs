using UnityEngine;
using System.Collections;

public class ThirdBoss : MonoBehaviour {
	public Transform target;
	public float speed = 4;
	public float minChargeTime = 2;
	public float maxChargeTime = 4;
	public float minSpinTime = 4;
	public float maxSpinTime = 8;
	public bool isCharging = false;

	// Use this for initialization
	void Start () {
		isCharging = false;
		Invoke("StartCharge", Random.Range(minSpinTime, maxSpinTime));
	}
	
	// Update is called once per frame
	void Update () {
		if (isCharging) 
		{
			rigidbody.AddForce(transform.forward * speed);
			//rigidbody.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
		} else {
			transform.LookAt(target);
		}
	}

	void StartCharge()
	{
		if (!isCharging) {
			Debug.Log(gameObject.name + " Starts Charging");
			isCharging = true;
			Invoke("StopCharging", Random.Range(minChargeTime, maxChargeTime));
		}
	}

	void StopCharging()
	{
		if (isCharging) {
			Debug.Log(gameObject.name + " Stops Charging");
			isCharging = false;
			rigidbody.velocity = Vector3.zero;
			Invoke("StartCharge", Random.Range(minSpinTime, maxSpinTime));
		}
	}
}
