using UnityEngine;
using System.Collections;

public class DamageOnCollide : MonoBehaviour {
	private bool canDealDamage = true;
	public int damageToDeal = 1;
	public float delayBetweenHits = 3; //Set to 0 to deactivate
	public CollisionTarget target;

	public enum CollisionTarget {Player, Enemy}

	void OnCollisionEnter(Collision c)
	{
		if (canDealDamage && c.gameObject.tag == target.ToString()) 
		{
			c.gameObject.SendMessage("TakeDamage", damageToDeal, SendMessageOptions.DontRequireReceiver);
			canDealDamage = false;
			Invoke("SetCanTakeDamage", delayBetweenHits);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (canDealDamage && hit.gameObject.tag == target.ToString()) 
		{
			hit.gameObject.SendMessage("TakeDamage", damageToDeal, SendMessageOptions.DontRequireReceiver);
			canDealDamage = false;
			if (delayBetweenHits > 0)
			{
				Invoke("SetCanDealDamage", delayBetweenHits);
			}
		}
	}


	void SetCanDealDamage()
	{
		canDealDamage = true;
	}
}
