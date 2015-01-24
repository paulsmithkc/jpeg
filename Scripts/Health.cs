using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int curHealth = 3;
	public int maxHealth = 3;

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
		if (curHealth <= 0)
		{
			Debug.Log(gameObject.name + " Died");
			gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
		}
	}
}
