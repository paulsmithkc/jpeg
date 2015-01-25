using UnityEngine;
using System.Collections;

public class SecondBoss : MonoBehaviour {
	private Animator animator;
	public float jumpStrength = .2f;
	private AudioSource audioSource;
	public AudioClip jumpSound;
	public AudioClip squishSound;
	public GameObject nextBoss;
	private Hud hud;
	public GameObject bodyMesh;

	void Start()
	{
		animator= GetComponent<Animator>();
		audioSource = gameObject.AddComponent<AudioSource>();
		hud = GameObject.Find("Hud").GetComponent<Hud>();
	}

	void LateUpdate()
	{
		transform.LookAt (transform.position + rigidbody.velocity);
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
	}


	public void DoJump()
	{
		Vector3 direction = new Vector3 (Random.Range (-1f, 1f)*jumpStrength, jumpStrength, Random.Range (-1f, 1f)*jumpStrength);
		rigidbody.velocity = direction;
		rigidbody.position = transform.position + (direction * Time.deltaTime);

		audioSource.PlayOneShot(jumpSound);
		animator.Play("walk");
	}

	public void DoIdle()
	{
		animator.Play("Idle");
	}

	public void Die()
	{
		hud.FadeTo(Color.white, 1);
		hud.FadeTo(Color.clear, 3);
		animator.SetTrigger("Squish");
		audioSource.PlayOneShot(squishSound);
		Instantiate(nextBoss, new Vector3(0, 10, 0), Quaternion.identity);
		Destroy(gameObject, 1);

		var m = bodyMesh.GetComponent<Renderer>().material;
		m.SetFloat("_DisplacementMagnitude", 0.2f);
		m.SetFloat("_DisplacementVerticalPeriod", 100.0f);
		m.SetFloat("_DisplacementAnimationPeriod", 2.0f);
	}
}
