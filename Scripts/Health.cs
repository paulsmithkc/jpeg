using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int curHealth = 3;
	public int maxHealth = 3;

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
		curHealth -= damage;
		if(curHealth <= 0)
		{
			BroadcastMessage("Die",gameObject,SendMessageOptions.DontRequireReceiver);
		}
	}
}
