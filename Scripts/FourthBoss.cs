using UnityEngine;
using System.Collections;

public class FourthBoss : MonoBehaviour 
{
	public float turnSpeed = 1;
	public float fireDuration = 3;
	public GameObject player;

	public Vector2 offset;

	void Update()
	{
		transform.rotation = Quaternion.LookRotation( Vector3.RotateTowards (transform.forward, (transform.position - player.transform.position).normalized, turnSpeed, 0.0f));
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
	}
}
