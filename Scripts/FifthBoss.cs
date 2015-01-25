using UnityEngine;
using System.Collections;

public class FifthBoss : MonoBehaviour 
{
	public float spinSpeed;
	public float minSpinTime = 4;
	public float maxSpinTime = 8;
	public ParticleSystem myParticleSystem;

	private Animator animator;
	private float spinTime = 0;
	private bool slamming = true;
	private AudioSource audioSource;
	public AudioClip squishSound;
	public GameObject nextBoss;
	private Hud hud;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
		hud = GameObject.Find("Hud").GetComponent<Hud>();
		Spin();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float deltaTime = Time.deltaTime;
		if (spinTime > deltaTime) 
		{
			transform.Rotate(new Vector3(0,1,0) * spinSpeed * deltaTime);
			spinTime -= deltaTime;
		}
		else if (!slamming)
		{
			Debug.Log(gameObject.name + " Starts Slamming");
			animator.SetTrigger("slamall");
			spinTime = 0.0f;
			slamming = true;
		}
	}

	void Spin()
	{
		if (slamming) 
		{
			spinTime = Random.Range(minSpinTime, maxSpinTime);
			slamming = false;
			Debug.Log(gameObject.name + " Starts Spinning For " + spinTime);
		}
	}

	void Spray() {
		Debug.Log(gameObject.name + " Slam!");
		myParticleSystem.Play();
	}

	public void Die()
	{
		hud.FadeTo(Color.white, hud.defaultFadeTime * 0.1f);
		hud.FadeTo(Color.clear, hud.defaultFadeTime * 0.9f);
		//animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);
		Instantiate(nextBoss, new Vector3(-5, 10, 0), Quaternion.identity);
		Instantiate(nextBoss, new Vector3(5, 10, 0), Quaternion.identity);
		Instantiate(nextBoss, new Vector3(0, 10, 5), Quaternion.identity);
		Destroy(gameObject, hud.defaultFadeTime * 0.1f);
	}
}
