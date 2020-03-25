using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

	[SerializeField] int startingHealth = 100;
	[SerializeField] float timeSinceLastHit = 2.0f;
	[SerializeField] int currentHealth;
	[SerializeField] Slider healthSlider;


	private CharacterMovement characterMovement;
	private float timer = 0f;
	private Animator anim;
	private AudioSource audio;
	public AudioClip hurtAudio;
	public AudioClip deadAudio;
	public AudioClip pickItem;
	private ParticleSystem particleSystem;

	public LevelManager levelManager;
	public bool isDead;

	public int CurrentHealth {
		get { return currentHealth; }
		set {
			if (value < 0)
				currentHealth = 0;
			else
				currentHealth = value;
		}
	}

	public Slider HealthSlider {
		get {return healthSlider; }
	}

	void Awake () {
		Assert.IsNotNull (healthSlider);
		particleSystem = GetComponent<ParticleSystem> ();
		particleSystem.enableEmission = false;
	}


	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		currentHealth = startingHealth;
		characterMovement = GetComponent<CharacterMovement> ();
		audio = GetComponent <AudioSource> ();
		levelManager = FindObjectOfType<LevelManager> ();
		isDead = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		PlayerKill ();
		
	}

	void OnTriggerEnter (Collider other) {

		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver) {

			if (other.tag == "Weapon") {
				takeHit ();
				timer = 0;
			}
		}
	}

	public void takeHit () {
		if (currentHealth >= 0) {
			GameManager.instance.PlayerHit (currentHealth);
			anim.Play ("Hurt");
			currentHealth -= 10;
			healthSlider.value = currentHealth;
			audio.PlayOneShot (hurtAudio);
		}

		if (currentHealth <= 0) {
			//GameManager.instance.PlayerHit (currentHealth);
			anim.SetTrigger ("isDead");
			characterMovement.enabled = false;
			audio.PlayOneShot (deadAudio);
		}
	}

	public void PowerUpHealth () {
		if (currentHealth <= 80) {
			currentHealth += 20;
		} else if (currentHealth < startingHealth) {
			CurrentHealth = startingHealth;
		}

		healthSlider.value = currentHealth;
		audio.PlayOneShot (pickItem);
	}

	public void KillBox () {
		CurrentHealth = 0;
		healthSlider.value = currentHealth;

	}

	public void PlayerKill () {
		if (currentHealth <= 0) {
			characterMovement.enabled = false;
			levelManager.RespawnPlayer ();
		}
	}
}
