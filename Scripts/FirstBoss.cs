using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour {
	GameObject player;

	void Start()
	{
		player = GameObject.Find ("Player");
	}
	void Die()
	{
		Destroy (player.GetComponent<DamageOnCollide> ());
		Destroy (gameObject);
	}
}
