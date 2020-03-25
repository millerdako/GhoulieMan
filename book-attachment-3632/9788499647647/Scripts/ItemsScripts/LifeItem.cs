using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour {

	private GameObject player;
	private AudioSource audio;
	private LifeManager lifeManager;
	private SpriteRenderer spriteRenderer;
	public GameObject pickupEffect;
	private BoxCollider boxCollider;
	private PowerItemExplode powerItemExplode;


	// Use this for initialization
	void Start () {

		player = GameManager.instance.Player;
		lifeManager = FindObjectOfType<LifeManager> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		powerItemExplode = GetComponent<PowerItemExplode> ();
		boxCollider = GetComponent<BoxCollider> ();

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject == player) {
			PickLife ();
			print ("Life Collected");

		}
	}

	public void PickLife () {

		lifeManager.GiveLife ();
		powerItemExplode.Pickup ();
		spriteRenderer.enabled = false;
		boxCollider.enabled = false;
		Destroy (gameObject);
	}



	void Pickup () {
		Instantiate (pickupEffect, transform.position, transform.rotation);
	}
}
