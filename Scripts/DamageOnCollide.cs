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
		OnTriggerEnter (c.collider);
	}

	void onCollisionStay(Collision c)
	{
		OnTriggerEnter (c.collider);
	}

	void OnTriggerStay(Collider c)
	{
		OnTriggerEnter (c);
	}

	void OnTriggerEnter(Collider c)
	{
		if (canDealDamage && c.gameObject.tag == target.ToString()) 
		{
			c.gameObject.transform.root.BroadcastMessage("TakeDamage", damageToDeal, SendMessageOptions.DontRequireReceiver);
			if (delayBetweenHits > 0)
			{
				canDealDamage = false;
				Invoke("SetCanDealDamage", delayBetweenHits);
			}
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		OnTriggerEnter(hit.collider);
	}


	void SetCanDealDamage()
	{
		canDealDamage = true;
	}
}
