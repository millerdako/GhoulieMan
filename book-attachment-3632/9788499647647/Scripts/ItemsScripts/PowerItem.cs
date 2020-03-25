using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : MonoBehaviour {

	private GameObject player;
	private AudioSource audio;
	private ParticleSystem particleSystem;
	private PlayerHealth playerHealth;

	private MeshRenderer meshRenderer;
	private ParticleSystem brainParticles;

	public GameObject pickupEffect;

	private PowerItemExplode powerItemExplode;
	private SphereCollider sphereCollider;

	// Use this for initialization
	void Start () {

		player = GameManager.instance.Player;
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerHealth.enabled = true;

		particleSystem = player.GetComponent<ParticleSystem> ();
		//particleSystem.enableEmission = false;

		meshRenderer = GetComponentInChildren <MeshRenderer> ();
		brainParticles = GetComponent<ParticleSystem> ();

		powerItemExplode = GetComponent<PowerItemExplode> ();
		sphereCollider = GetComponent<SphereCollider> ();


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject == player) {

			StartCoroutine (InvincilbeRoutine ());
			meshRenderer.enabled = false;
			
		}
	}

	public IEnumerator InvincilbeRoutine () {


		print ("pick PowerItem");
		powerItemExplode.Pickup ();
		particleSystem.enableEmission = true;
		playerHealth.enabled = false;
		brainParticles.enableEmission = false;
		sphereCollider.enabled = false;

		yield return new WaitForSeconds (10f);
		print ("no more invencible");
		particleSystem.enableEmission = false;
		playerHealth.enabled = true;
		Destroy (gameObject);
	}

	void Pickup () {
		Instantiate (pickupEffect, transform.position, transform.rotation);
	}
}
