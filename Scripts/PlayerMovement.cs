﻿using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour {
	public float speed = 5;
	public Vector3 vel;
	public float gravity = 9.86f;
	public float jumpSpeed = 2;
	public Camera playerCamera;

	public bool invertY = true;

	private CharacterController cc;
	private float mouseLookUp = 0;
	private AudioSource audioSource;
	public AudioClip jumpSound;
	public AudioClip music;
	public float musicVolume = 1.0f;

	void Start () 
	{
		cc = GetComponent<CharacterController>();
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.clip = music;
		audioSource.volume = musicVolume;
		audioSource.Play();
	}

    void PickUpItem(string Item)
    {
        Debug.Log("Player picked up " + Item);
    }

	// Update is called once per frame
	void Update () 
	{

		Vector3 moveDirection = Vector3.zero;
		moveDirection.z = Input.GetAxis ("Vertical");
		moveDirection.x = Input.GetAxis ("Horizontal");

		if (moveDirection.magnitude > 1) 
		{
			moveDirection.Normalize();
		}

		if (invertY) {
			mouseLookUp += Input.GetAxis ("Mouse Y");
		} else {
			mouseLookUp -= Input.GetAxis ("Mouse Y");
		}
		mouseLookUp = Mathf.Clamp (mouseLookUp, -90, 90);
		playerCamera.transform.localEulerAngles = new Vector3(mouseLookUp, 0, 0);

		transform.eulerAngles += new Vector3(0,Input.GetAxis("Mouse X"),0);

		if(Input.GetButtonDown("Jump") && cc.isGrounded)
		{
			vel.y = jumpSpeed;
			audioSource.PlayOneShot(jumpSound);
		}
		vel.y -= gravity * Time.deltaTime;

		cc.Move(((transform.TransformDirection(moveDirection) * speed) + vel ) * Time.deltaTime);
		if (cc.isGrounded) 
		{
			vel.y = 0;
		}
	}
}
