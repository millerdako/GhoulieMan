using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour {

	private GameObject player;
	private CharacterMovement characterMovement;

	private SpriteRenderer spriteRenderer;

	public GameObject pickUpEffect;

	private PowerItemExplode powerItemExplode;
	private SphereCollider sphereCollider;


	// Use this for initialization
	void Start () {

		player = GameManager.instance.Player;
		characterMovement = player.GetComponent<CharacterMovement> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

		powerItemExplode = GetComponent <PowerItemExplode> ();
		sphereCollider = GetComponent <SphereCollider> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject == player) {
			
			StartCoroutine (JumpRoutine ());
		}
	}

	public IEnumerator JumpRoutine () {

		powerItemExplode.Pickup ();
		characterMovement.jumpSpeed = 800;
		spriteRenderer.enabled = false;
		sphereCollider.enabled = false;

		yield return new WaitForSeconds (10f);
		print ("No more jump");
		characterMovement.jumpSpeed = 600;
		Destroy (gameObject);

	}

	void PickUp () {
		Instantiate (pickUpEffect, transform.position, transform.rotation);
	}
}
