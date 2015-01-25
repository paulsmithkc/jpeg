using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int curHealth = 1;
	public int maxHealth = 1;
	public bool dead = false;

	public bool destroyOnDeath = false;

	Hud hud;

	void Start() {
		curHealth = maxHealth;
		hud = GameObject.Find("Hud").GetComponent<Hud>();
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
		if (GetComponent<Health> ().enabled) 
		{
			Debug.Log(gameObject.name + " Took Damage");
			curHealth -= damage;
			if(gameObject.tag == "Player")
			{
				hud.FadeTo(Color.red, .1f);
				hud.FadeTo(Color.clear, .3f);
			}
			if (curHealth <= 0 && !dead) 
			{
					Debug.Log (gameObject.name + " Died");
					if(gameObject != gameObject.transform.root)
					{
						gameObject.transform.root.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
					}else{
						gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
					}
					if (destroyOnDeath) {
							Destroy (gameObject);
					}
					dead = true;
			}
		}
	}

}
