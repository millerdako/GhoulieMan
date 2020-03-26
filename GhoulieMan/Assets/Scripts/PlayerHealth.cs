using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLasHit = 2.0f;
    [SerializeField] int currentHealth;
    [SerializeField] private float timer = 0f;
    [SerializeField] Slider healthSlider;
    private Animator anim;
    private CharacterMovement characterMovement;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip dieAudio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
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
            healthSlider.value = currentHealth;
            audio.PlayOneShot (hurtAudio);
        }
        
        if(currentHealth <= 0){
            GameManager.instance.PlayerHit (currentHealth);
            anim.SetTrigger ("isDead");
            characterMovement.enabled = false;
            audio.PlayOneShot (dieAudio);
        }
    }
}
