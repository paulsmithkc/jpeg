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

	public float lazerStartHeight = 0;

	private Animator animator;
	private AudioSource audioSource;
	public AudioClip squishSound;
	public GameObject nextBoss;
	private Hud hud;

	void Start()
	{
		player = GameObject.Find("Player");
		animator = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
		hud = GameObject.Find("Hud").GetComponent<Hud>();
		TrackPlayer();
		lazerStartHeight = lazerObject.transform.localPosition.y;
	}

	void Update()
	{
		if (trackPlayer) {
						transform.rotation = Quaternion.LookRotation (Vector3.RotateTowards (transform.forward, (transform.position - player.transform.position).normalized, turnSpeed, 0.0f));
						transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
						lazerObject.SetActive (false);
				} else if (chargeing) {
						//chargeObject.transform.localScale += new Vector3(1,1,1) * Time.deltaTime;
				} else {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, -transform.forward, out hit, Mathf.Infinity, layersToCheck)) {
				lazerObject.transform.localScale = new Vector3 (lazerObject.transform.localScale.x, Vector3.Distance (transform.position, hit.point) / 2, lazerObject.transform.localScale.z);
				lazerObject.transform.position = ((transform.position + hit.point) / 2);
				lazerObject.transform.localPosition = new Vector3 (lazerObject.transform.localPosition.x, lazerStartHeight, lazerObject.transform.localPosition.z);
			}
				}
	}

	void TrackPlayer()
	{
		GetComponent<Health> ().enabled = false;
//		chargeObject.transform.localScale = Vector3.zero;
		animator.Play("VisorClose");
		Debug.Log ("Playing Close");
		trackPlayer = true;
		chargeing = false;
		lazerObject.SetActive(false);
		Invoke ("ChargeAttack", trackPlayerDuration);
	}

	void ChargeAttack()
	{
		GetComponent<Health> ().enabled = true;
		animator.Play ("VisorOpen");
		Debug.Log ("Playing Open");
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
		Invoke ("TrackPlayer", fireDuration);
	}

	public void Die()
	{
		hud.FadeTo(Color.white, hud.defaultFadeTime * 0.1f);
		hud.FadeTo(Color.clear, hud.defaultFadeTime * 0.9f);
		//animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);
		Instantiate(nextBoss, new Vector3(0, 10, 0), Quaternion.identity);
		Destroy(gameObject, hud.defaultFadeTime * 0.1f);
	}
}
