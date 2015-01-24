using UnityEngine;
using System.Collections;

public class DamageOnCollide : MonoBehaviour {
	private bool canTakeDamage = true;
	public int damageToDeal = 1;
	public float delayBetweenHits = 3; //Set to 0 to deactivate
	public CollisionTarget target;

	public enum CollisionTarget{Player, Enemy}

	void OnCollisionEnter(Collision c)
	{
		if (canTakeDamage && c.collider.tag == target.ToString()) 
		{
			c.collider.SendMessage("TakeDamage", damageToDeal);
			canTakeDamage = false;
			Invoke("SetCanTakeDamage", delayBetweenHits);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (canTakeDamage && hit.transform.tag == target.ToString()) 
		{
			hit.transform.SendMessage("TakeDamage", damageToDeal);
			canTakeDamage = false;

			if(delayBetweenHits > 0)
			{
				Invoke("SetCanTakeDamage", delayBetweenHits);
			}
		}
	}


	void SetCanTakeDamage()
	{
		canTakeDamage = true;
	}
}
