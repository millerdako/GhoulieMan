using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Health : MonoBehaviour {

	[SerializeField] private int startingHealth = 10;
	[SerializeField] private int currentHealth;

	private Rigidbody rigidbody;
	private SphereCollider sphereCollider;
	private AudioSource audio;
	public AudioClip killAudio;
	public GameObject explosionEffect;



	// Use this for initialization
	void Start () {

		rigidbody = GetComponent <Rigidbody> ();
		sphereCollider = GetComponent <SphereCollider> ();
		currentHealth = startingHealth;
		audio = GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other) {

		if ( !GameManager.instance.GameOver) {
			if (other.tag == "PlayerWeapon") {
				takeHit ();
			}
		}
	}

	void takeHit () {

		if (currentHealth > 0) {
			GameObject newexplosionEffect = (GameObject)Instantiate (explosionEffect, transform.position, transform.rotation);
			Destroy (newexplosionEffect, 1);
			currentHealth -= 10;
		}

		if (currentHealth <= 0) {
			KillEnemy ();
		}
	}

	void KillEnemy () {

		sphereCollider.enabled = false;
		audio.PlayOneShot (killAudio);
		Destroy (gameObject);

	}


}
