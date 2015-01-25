using UnityEngine;
using System.Collections;

public class SecondBoss : MonoBehaviour {
	private Animator ani;
	public float jumpStrength = .2f;
	private AudioSource audioSource;
	public AudioClip jumpSound;

	void Start()
	{
		ani = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
	}

	void LateUpdate()
	{
		transform.LookAt (transform.position + rigidbody.velocity);
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
	}


	public void DoJump()
	{
		Vector3 direction = new Vector3 (Random.Range (-1f, 1f)*jumpStrength, jumpStrength, Random.Range (-1f, 1f)*jumpStrength);
		rigidbody.velocity = direction;
		rigidbody.position = transform.position + (direction * Time.deltaTime);

		audioSource.PlayOneShot(jumpSound);
		ani.Play("walk");
	}

	public void DoIdle()
	{
		ani.Play ("Idle");
	}
}
