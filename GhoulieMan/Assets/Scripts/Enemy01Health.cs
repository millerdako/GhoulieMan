using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{
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

    public bool IsAlive {
        get {return isAlive;}
    }
    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponentInChildren<BoxCollider>();
        rigidbody = GetComponent <Rigidbody> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        nav = GetComponent <NavMeshAgent> ();
        anim = GetComponent <Animator> ();
        isAlive = true;
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(dissapearEnemy){
            transform.Translate (-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter (Collider other){
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver){
            if(other.tag == "PlayerWeapon"){
                takeHit();
                timer = 0f;
            }
        }
    }

    void takeHit (){
        if(currentHealth > 0){
            anim.Play ("Hurt");
            currentHealth -= 10;
        }

        if (currentHealth <= 0){
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy(){
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger ("EnemyDie");
        rigidbody.isKinematic = true;
        weaponCollider.enabled = false;
        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy(){
        yield return new WaitForSeconds (2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds (1f);
        Destroy (gameObject);
    }
}
