using UnityEngine;
using System.Collections;

public class SecondBoss : MonoBehaviour {
	Animator ani;
	public float jumpStrength = .2f;

	void Start()
	{
		ani = GetComponent<Animator>();
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

		ani.Play("walk");
	}

	public void DoIdle()
	{
		ani.Play ("Idle");
	}
}
