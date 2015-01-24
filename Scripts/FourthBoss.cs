using UnityEngine;
using System.Collections;

public class FourthBoss : MonoBehaviour 
{
	public float turnSpeed = 1;
	public float trackPlayerDuration = 5;
	public float chargeDuration = 3;
	public float fireDuration = 3;
	public GameObject player;

	public GameObject chargeObject;
	public GameObject lazerObject;
	public LayerMask layersToCheck;

	public bool trackPlayer = true;
	public bool chargeing = false;

	void Start()
	{
		TrackPlayer ();
	}

	void Update()
	{
		if (trackPlayer) 
		{

			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (transform.position - player.transform.position).normalized, turnSpeed, 0.0f));
			transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
			lazerObject.SetActive (false);
		} else if (chargeing) {
			//chargeObject.transform.localScale += new Vector3(1,1,1) * Time.deltaTime;
		}
	}

	void TrackPlayer()
	{
		chargeObject.transform.localScale = Vector3.zero;
		GetComponent<Animator> ().Play ("VisorClose");
		GetComponent<Animator> ().Play ("Blink");
		trackPlayer = true;
		chargeing = false;
		lazerObject.SetActive(false);
		Invoke ("ChargeAttack", trackPlayerDuration);
	}

	void ChargeAttack()
	{
		GetComponent<Animator> ().Play ("VisorOpen");
		trackPlayer = false;
		chargeing = true;
		lazerObject.SetActive(false);
		Invoke ("FireLazer", chargeDuration);
	}

	void FireLazer()
	{
		trackPlayer = false;
		chargeing = false;
		lazerObject.SetActive(true);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.forward, out hit, Mathf.Infinity, layersToCheck)) {
			lazerObject.transform.localScale = new Vector3 (lazerObject.transform.localScale.x, Vector3.Distance (transform.position, hit.point) / 2, lazerObject.transform.localScale.z);
			lazerObject.transform.position = ((transform.position + hit.point) / 2) + new Vector3 (0, lazerObject.transform.position.y);
		}
		Invoke ("TrackPlayer", fireDuration);
	}
}
