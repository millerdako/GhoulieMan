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
    public AudioClip pickItem;
    private ParticleSystem particleSystem;
    
    

    public int CurrentHealth {
        get { return currentHealth; }
        set {
            if(value < 0)
                currentHealth = 0;
            else
                currentHealth = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator> ();
        currentHealth = startingHealth;
        characterMovement = GetComponent<CharacterMovement> ();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter (Collider other) {
        if(timer >= timeSinceLasHit && !GameManager.instance.GameOver && (particleSystem.enableEmission == false)){
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

    public void PowerUpHealth () {
        if (currentHealth <= 80) {
            currentHealth += 20;
        } else if (currentHealth < startingHealth) {
            CurrentHealth = startingHealth;
        }
        healthSlider.value = currentHealth;
        audio.PlayOneShot (pickItem);
    }

    public void InvencibleItem () {
        audio.PlayOneShot (pickItem);
    }

    public void KillBox (){
        CurrentHealth = 0;
        healthSlider.value = currentHealth;
        //takeHit ();
    }
}
