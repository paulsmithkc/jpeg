using UnityEngine;
using System.Collections;

public class FifthBoss : MonoBehaviour 
{
	public float spinSpeed;

	public float minSpinTime;
	public float maxSpinTime;

	public float minTimeDown;
	public float maxTimeDown;

	public bool isSpinning = true;

	// Use this for initialization
	void Start () 
	{
		Spin ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isSpinning) 
		{
			transform.Rotate(new Vector3(0,1,0) * spinSpeed *Time.deltaTime);
		}
	}

	void Spin()
	{
		isSpinning = true;
		GetComponent<Animator> ().Play ("Idle");
		Invoke ("Slam", Random.Range (minSpinTime, maxSpinTime));
	}

	void Slam()
	{
		isSpinning = false;
		GetComponent<Animator> ().Play ("slamall");
		Invoke("WaitSlam", Random.Range(minTimeDown, maxTimeDown));
	}

	void WaitSlam()
	{
		//Invoke ("Spin", Random.Range(minSpinTime, maxSpinTime));
		GetComponent<Animator> ().Play ("readyslamall");
	}
}
