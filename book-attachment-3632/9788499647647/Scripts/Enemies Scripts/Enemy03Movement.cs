using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Movement : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody rigidBody;
	private Transform transform;

	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody> ();
		transform = GetComponent<Transform> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 Vel = rigidBody.velocity;
		Vel.x = - moveSpeed;
		rigidBody.velocity = Vel;

	}
}
