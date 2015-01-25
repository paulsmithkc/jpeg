using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public string ItemType = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.root.SendMessage("PickUpItem", ItemType);
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.transform.root.SendMessage("PickUpItem", ItemType);
            Destroy(gameObject);
        }
    }

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.transform.tag == "Player")
		{
			hit.transform.root.SendMessage("PickUpItem", ItemType);
			Destroy(gameObject);
		}
	}
}
