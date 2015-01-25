using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour {
	private GameObject player;
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
	}

	public void Die()
	{
		//Destroy(player.GetComponent<DamageOnCollide>());
		//hud.FadeTo(Color.white, hud.defaultFadeTime * 0.1f);
		//hud.FadeTo(Color.clear, hud.defaultFadeTime * 0.9f);
		animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);
		Instantiate(nextBoss, new Vector3(0, 10, 0), Quaternion.identity);
		Destroy(gameObject, 5);
	}

	void OnCollisionEnter(Collision c)
	{
		animator.SetTrigger("Land");
	}
}
