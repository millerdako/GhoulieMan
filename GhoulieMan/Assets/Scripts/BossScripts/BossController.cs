using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossAwake = false;
    private Animator anim;

    public bool inBattle = false;
    public bool attacking = false;
    public float idleTimer = 0.0f;
    public float idleWaitTime = 10.0f;
    private BossHealth bossHealth;
    public float attackTimer = 0.0f;
    public float attackWaitTime = 2.0f;
    private BoxCollider swordTrigger;
    public GameObject bossHealthBar;
    private SmoothFollow smoothFollow;
    private GameObject player;
    private PlayerHealth playerHealth;
    private BoxCollider bossCheckPoint;
    private ParticleSystem particleSystem;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bossHealth = GetComponentInChildren<BossHealth>();
        swordTrigger = GameObject.Find("Boss").GetComponentInChildren<BoxCollider>();
        bossHealthBar.SetActive(false);
        smoothFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        bossCheckPoint = GameObject.FindGameObjectWithTag("BossCheckPoint").GetComponent<BoxCollider>();
        particleSystem = GameObject.Find("RockPS").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossAwake)
        {
            //print("Boss is awake!");
            anim.SetBool("bossAwake", true);
            bossHealthBar.SetActive(true);

            if (inBattle)
            {
                if (!attacking)
                {
                    idleTimer += Time.deltaTime;
                }
                else
                {
                    idleTimer = 0.0f;
                    attackTimer += Time.deltaTime;
                    if(attackTimer>= attackWaitTime)
                    {
                        BossAttack1();
                    }
                }
                if(idleTimer >= idleWaitTime)
                {
                    //print("Boss is attacking!");
                    attacking = true;
                    idleTimer = 0.0f;
                }
            }
        }
        BossReset();
    }

    void BossReset()
    {
        if (playerHealth.CurrentHealth <= 0)
        {
            bossAwake = false;
            bossCheckPoint.isTrigger = true;
            //print("Boss is sleeping again");
            smoothFollow.bossCameraActive = false;
            anim.Play("BossIdle");
            anim.SetBool("bossAwake", false);
            bossHealth.bossHealth = 20;
        }
    }

    void BossAttack1()
    {
        attacking = false;
        anim.SetTrigger("bossAttack1");
        attackTimer = 0.0f;
        //print("Boss Attack 1");
        swordTrigger.enabled = true;
        //print("SwordTrigger is enabled");
    }

    void BossAttack2()
    {
        attacking = false;
        anim.SetTrigger("bossAttack2");
        attackTimer = 0.0f;
        swordTrigger.enabled = true;
        //print("Boss Attack 2");
    }

    void BossAttack3()
    {
        attacking = false;
        anim.SetTrigger("bossAttack3");
        attackTimer = 0.0f;
        swordTrigger.enabled = true;
        StartCoroutine(FallingRocks());
        //print("Boss Attack 3");
    }

    IEnumerator FallingRocks()
    {
        yield return new WaitForSeconds(2);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        particleSystem.enableEmission = true;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        particleSystem.Play();
        yield return new WaitForSeconds(3);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        particleSystem.enableEmission = false;
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
    }
}
