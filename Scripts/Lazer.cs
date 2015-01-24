using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {
	public Vector2 offset;
	public float speed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		offset.y += Time.deltaTime*speed;
		renderer.material.mainTextureOffset = offset;
	}
}
