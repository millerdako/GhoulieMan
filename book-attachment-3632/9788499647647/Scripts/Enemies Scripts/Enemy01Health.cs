using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour {

	[SerializeField] private int startingHealth = 20;
	[SerializeField] private float timeSinceLastHit = 0.5f;
	[SerializeField] private float dissapearSpeed = 2f;
	[SerializeField] private int currentHealth;


	private float timer = 0f;
	private Animator anim;
	private NavMeshAgent nav;
	private bool isAlive;
	private Rigidbody rigidbody;
	private CapsuleCollider capsuleCollider;
	private bool dissapearEnemy = false;
	private BoxCollider weaponCollider;
	private AudioSource audio;
	public AudioClip hurtAudio;
	public AudioClip killAudio;

	public bool IsAlive {
		get {return isAlive; }
	}

	private DropItem dropItem;

	// Use this for initialization
	void Start () {

		rigidbody = GetComponent <Rigidbody> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
		isAlive = true;
		currentHealth = startingHealth;
		weaponCollider = GetComponentInChildren <BoxCollider> ();
		audio = GetComponent<AudioSource> ();
		dropItem = GetComponent<DropItem> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (dissapearEnemy) {
			transform.Translate (-Vector3.up * dissapearSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter (Collider other) {

		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver) {
			if (other.tag == "PlayerWeapon") {
				takeHit ();
				timer = 0f;
			}
		}
	}

	void takeHit () {

		if (currentHealth > 0) {
			anim.Play ("Hurt");
			currentHealth -= 10;
			audio.PlayOneShot (hurtAudio);
		}

		if (currentHealth <= 0) {
			isAlive = false;
			KillEnemy ();
		}
	}

	void KillEnemy () {

		capsuleCollider.enabled = false;
		nav.enabled = false;
		anim.SetTrigger ("EnemyDie");
		rigidbody.isKinematic = true;
		weaponCollider.enabled = false;
		audio.PlayOneShot (killAudio);

		StartCoroutine (removeEnemy ());
		print ("Drop Item");
		dropItem.Drop ();
	}

	IEnumerator removeEnemy() {

		yield return new WaitForSeconds (2f);
		dissapearEnemy = true;
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}
