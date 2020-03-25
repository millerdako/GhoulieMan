using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public bool bossAwake = false;
	private Animator anim;
	

	public bool inBattle = false;
	public bool attacking = false;
	public float idleTimer = 0.0f;
	public float idleWaitTime = 10.0f;


	public float attackTimer = 0.0f;
	public float attackWaitTime = 4.0f;
	public int attackCount = 1;

	public GameObject bossHealthBar;
	private BoxCollider swordTrigger;

	private SmoothFollow smoothFollow;
	private GameObject player;
	private PlayerHealth playerHealth;

	private BoxCollider bossCheckPoint;
	private BossHealth bossHealth;

	private ParticleSystem particleSystem;




	// Use this for initialization
	void Start () {

		
		anim = GetComponent<Animator> ();	
		bossHealthBar.SetActive (false);
		swordTrigger = GameObject.Find ("Boss").GetComponentInChildren<BoxCollider>();
		smoothFollow= GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SmoothFollow>();
		player = GameManager.instance.Player;
		playerHealth = player.GetComponent<PlayerHealth>();
		bossCheckPoint = GameObject.Find ("BossCheckPoint").GetComponent<BoxCollider>();
		bossHealth = GetComponentInChildren<BossHealth>();
		particleSystem = GameObject.Find("Rock_PS").GetComponent<ParticleSystem>();
	

	}
	
	// Update is called once per frame
	void Update () {

        if(bossAwake)
        {
            print("Boss is awake");
			anim.SetBool("bossAwake", true);
			bossHealthBar.SetActive (true);

			if (inBattle) {
				if(!attacking) {
					idleTimer += Time.deltaTime;
				} else {
					idleTimer = 0.0f;
					attackTimer += Time.deltaTime;
					if (attackTimer >= attackWaitTime) {
						switch (Random.Range (0,3)){
							case 0:
							BossAttack01();
							break;
							case 1:
							BossAttack02();
							break;
							case 2:
							BossAttack03();
							break;
							default: break;
						}
					}
				}
				if(idleTimer >= idleWaitTime ){
					print ("Boss attack 01");
					attacking = true;
					idleTimer = 0.0f;
				}
			}
			else {
				idleTimer = 0.0f;
			}
				if (bossHealth.bossHealth > 0 && playerHealth.CurrentHealth > 0){
					if (bossHealth.bossHealth > 15){
						attackWaitTime = 4.0f;
					}
					if (bossHealth.bossHealth > 10 && bossHealth.bossHealth < 16){
						attackWaitTime = 3.0f;
					}
					if (bossHealth.bossHealth > 5 && bossHealth.bossHealth < 11){
						attackWaitTime = 2.0f;
					}
					if (bossHealth.bossHealth >= 1 && bossHealth.bossHealth < 6){
						attackWaitTime = 1.0f;
					}
				}
        }
		BossReset ();		
	}

	void BossReset () {
		if (playerHealth.CurrentHealth == 0) {
			bossAwake = false;
			bossCheckPoint.isTrigger = true;
			print ("Boss is sleeping again");
			smoothFollow.bossCameraActive = false;
			anim.Play("Idle");
			anim.SetBool("bossAwake",false);
			bossHealth.bossHealth = 20;
			bossHealthBar.SetActive (false);
			swordTrigger.enabled = false;
		}

	}

	void BossAttack01 (){
		attacking = false;
		anim.SetTrigger ("bossAttack");
		attackTimer = 0.0f;
		print ("Boss Attack 01");
		swordTrigger.enabled = true;

	}

	void BossAttack02(){
		attacking = false;
		anim.SetTrigger ("bossAttack02");
		attackTimer = 0.0f;
		print ("Boss Attack 02");
		swordTrigger.enabled = true;
	}

	void BossAttack03(){
		attacking = false;
		anim.SetTrigger ("bossAttack03");
		attackTimer = 0.0f;
		print ("Boss Attack 03");
		swordTrigger.enabled = true;
		StartCoroutine (fallingRocks ());
		
		
	}

	IEnumerator fallingRocks (){
		yield return new WaitForSeconds (2);
		particleSystem.enableEmission = true;
		particleSystem.Play();
		yield return new WaitForSeconds (3);
		particleSystem.enableEmission = false;
	}

}
