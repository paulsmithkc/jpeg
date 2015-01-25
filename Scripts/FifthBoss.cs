using UnityEngine;
using System.Collections;

public class FifthBoss : MonoBehaviour 
{
	public float spinSpeed;
	public float minSpinTime;
	public float maxSpinTime;
	public ParticleSystem particleSystem;

	private Animator animator;
	private float spinTime = 0;
	private bool slamming = true;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();
		Spin();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float deltaTime = Time.deltaTime;
		if (spinTime > deltaTime) 
		{
			transform.Rotate(new Vector3(0,1,0) * spinSpeed * deltaTime);
			spinTime -= deltaTime;
		}
		else if (!slamming)
		{
			Debug.Log(gameObject.name + " Starts Slamming");
			animator.SetTrigger("slamall");
			spinTime = 0.0f;
			slamming = true;
		}
	}

	void Spin()
	{
		if (slamming) 
		{
			spinTime = Random.Range(minSpinTime, maxSpinTime);
			slamming = false;
			Debug.Log(gameObject.name + " Starts Spinning For " + spinTime);
		}
	}

	void Spray() {
		Debug.Log(gameObject.name + " Slam!");
		particleSystem.Play();
	}

}
