using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterMovement : MonoBehaviour {

	public float maxSpeed = 6.0f;
	public bool facingRight = true;
	public float moveDirection;
	private Rigidbody rigidbody;
	private Animator anim;

	public float jumpSpeed = 600.0f;
	public bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public float knifeSpeed = 600.0f;
	public Transform knifeSpawn;
	public Rigidbody knifePrefab;

	private AudioSource audio;
	public AudioClip projectileAudio;

	Rigidbody clone;


	void Awake (){
		groundCheck = GameObject.Find ("GroundCheck").transform;
		knifeSpawn = GameObject.Find ("KnifeSpawn").transform;
	}

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent <Rigidbody> ();
		anim = GetComponent<Animator> ();
		audio = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		moveDirection = CrossPlatformInputManager.GetAxis ("Horizontal");

		if (grounded && CrossPlatformInputManager.GetButtonDown ("Jump")) {
			anim.SetTrigger ("IsJumping");
			rigidbody.AddForce (new Vector2 (0, jumpSpeed));
		}
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		rigidbody.velocity = new Vector2 (moveDirection * maxSpeed, rigidbody.velocity.y);

		if (moveDirection > 0.0f && !facingRight) {
			Flip ();
		} else if (moveDirection < 0.0f && facingRight) {
			Flip ();
		}
		anim.SetFloat ("Speed", Mathf.Abs (moveDirection));

		if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
			Attack ();
		}

	}
		

	void Flip() {
		facingRight = !facingRight;
		transform.Rotate (Vector3.up, 180.0f, Space.World);
	}

	void Attack (){
		anim.SetTrigger ("attacking");


	}

	public void EnableCharacterMovement(){
		//characterMovement.enabled = true;
		print ("character movement enable");
	}

	public void CallFireProjectile(){
		clone = Instantiate (knifePrefab, knifeSpawn.position, knifeSpawn.rotation) as Rigidbody;
		clone.AddForce (knifeSpawn.transform.right * knifeSpeed);
		audio.PlayOneShot (projectileAudio);
	}


}
