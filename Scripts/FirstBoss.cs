using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour {
	private GameObject player;
	private Animator animator;

	void Start()
	{
		player = GameObject.Find ("Player");
		animator = GetComponent<Animator>();
	}

	void Die()
	{
		Destroy(player.GetComponent<DamageOnCollide>());
		animator.SetTrigger("Squish");
		//Destroy(gameObject);
	}

	void OnCollisionEnter(Collision c)
	{
		animator.SetTrigger("Land");
	}
}
