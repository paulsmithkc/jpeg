﻿using UnityEngine;
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
	private Hud hud;
	public GameObject bodyMesh;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player").transform;
		animator = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
		hud = GameObject.Find("Hud").GetComponent<Hud>();
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
			animator.Play("CHARGERUN");
			Debug.Log(gameObject.name + " Starts Charging");
			isCharging = true;
			Invoke("StopCharging", Random.Range(minChargeTime, maxChargeTime));
		}
	}

	void StopCharging()
	{
		if (isCharging) {
			animator.Play ("DANCING");
			Debug.Log(gameObject.name + " Stops Charging");
			isCharging = false;
			rigidbody.velocity = Vector3.zero;
			Invoke("StartCharge", Random.Range(minSpinTime, maxSpinTime));
		}
	}

	public void Die()
	{
		hud.FadeTo(Color.white, 1);
		hud.FadeTo(Color.clear, 3);
		animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);
		Instantiate(nextBoss, new Vector3(0, 10, 0), Quaternion.identity);
		Destroy(gameObject, 1);

		var m = bodyMesh.GetComponent<Renderer>().material;
		m.SetFloat("_DisplacementMagnitude", 0.2f);
		m.SetFloat("_DisplacementVerticalPeriod", 100.0f);
		m.SetFloat("_DisplacementAnimationPeriod", 2.0f);
	}
}
