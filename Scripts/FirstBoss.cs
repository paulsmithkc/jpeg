using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour {
	private GameObject player;
	private Animator animator;
	private AudioSource audioSource;
	public AudioClip squishSound;
	public GameObject nextBoss;

	void Start()
	{
		player = GameObject.Find("Player");
		animator = GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
	}

	public void Die()
	{
		//Destroy(player.GetComponent<DamageOnCollide>());
		animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);
		Instantiate(nextBoss, new Vector3(0, 10, 0), Quaternion.identity);
	}

	void OnCollisionEnter(Collision c)
	{
		animator.SetTrigger("Land");
	}
}
