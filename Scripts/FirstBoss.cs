using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour {
	private GameObject player;
	private Animator animator;
	private AudioSource audioSource;
	public AudioClip squishSound;

	void Start()
	{
		player = GameObject.Find ("Player");
		animator = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
	}

	void Die()
	{
		Destroy(player.GetComponent<DamageOnCollide>());
		animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);

		//Destroy(gameObject);
	}

	void OnCollisionEnter(Collision c)
	{
		animator.SetTrigger("Land");
	}
}
