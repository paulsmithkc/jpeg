using UnityEngine;
using System.Collections;

public class ThirdBoss : MonoBehaviour {
	public Transform target;
	private Animator animator;
	public float speed = 4;
	public float minChargeTime = 2;
	public float maxChargeTime = 4;
	public float minSpinTime = 4;
	public float maxSpinTime = 8;
	public bool isCharging = false;
	private AudioSource audioSource;
	public AudioClip squishSound;
	public GameObject nextBoss;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player").transform;
		animator = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
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

	public void Die()
	{
		//animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);
		Instantiate(nextBoss, new Vector3(0, 10, 0), Quaternion.identity);
		Destroy(gameObject);
	}
}
