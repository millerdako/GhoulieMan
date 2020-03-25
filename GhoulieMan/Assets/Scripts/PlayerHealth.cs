using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLasHit = 2.0f;
    [SerializeField] int currentHealth;
    [SerializeField] private float timer = 0f;
    private Animator anim;
    private CharacterMovement characterMovement;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        currentHealth = startingHealth;
        characterMovement = GetComponent<CharacterMovement> ();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter (Collider other) {
        if(timer >= timeSinceLasHit && !GameManager.instance.GameOver){
            if (other.tag=="Weapon"){
                takeHit ();
                timer = 0;
            }
        }
    }

    void takeHit () {
        if(currentHealth > 0){
            GameManager.instance.PlayerHit (currentHealth);
            anim.Play ("Hurt");
            currentHealth -= 10;
        }
        
        if(currentHealth <= 0){
            GameManager.instance.PlayerHit (currentHealth);
            anim.SetTrigger ("isDead");
            characterMovement.enabled = false;
        }
    }
}
