using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int curHealth = 1;
	public int maxHealth = 1;
	public bool dead = false;

	public bool destroyOnDeath = false;

	void Start() {
		curHealth = maxHealth;
	}

	void AddHealth(int amount)
	{
		curHealth += amount;
		if (curHealth > maxHealth) 
		{
			curHealth = maxHealth;
		}
	}

	void TakeDamage(int damage)
	{
		Debug.Log(gameObject.name + " Took Damage");
		curHealth -= damage;
		if (curHealth <= 0 && !dead)
		{
			Debug.Log(gameObject.name + " Died");
			gameObject.transform.root.BroadcastMessage("Die", SendMessageOptions.DontRequireReceiver);
			if(destroyOnDeath)
			{
				Destroy(gameObject);
			}
			dead = true;
		}
	}

}
