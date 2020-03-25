using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

	private CharacterMovement characterMovement;
	private Animator playerAnim;
	private BossController bossController;



	void Awake () {
		characterMovement = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement> ();
		playerAnim = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();
		bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FireProjectile (){
		characterMovement.CallFireProjectile ();
	}

	public void EnableBossBattle () {
		characterMovement.enabled = true;
		playerAnim.Play("Blend Tree");
		bossController.inBattle = true;

		}

}
