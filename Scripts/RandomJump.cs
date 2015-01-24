using UnityEngine;
using System.Collections;

public class RandomJump : MonoBehaviour {
	Animator ani;
	public float jumpStrength = .2f;

	void Start()
	{
		ani = GetComponent<Animator>();
		/*DoIdle();
		ani.StartPlayback();*/
	}


	public void DoJump()
	{
		Vector3 direction = new Vector3 (Random.Range (-1f, 1f)*jumpStrength, jumpStrength, Random.Range (-1f, 1f)*jumpStrength);
		rigidbody.AddForce(direction);
		transform.LookAt (transform.position + direction);
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
		ani.Play("walk");
	}

	public void DoIdle()
	{
		ani.Play ("Idle");
	}
}
