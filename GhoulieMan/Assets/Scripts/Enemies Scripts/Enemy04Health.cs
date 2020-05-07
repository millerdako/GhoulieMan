using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private int currentHealth;

    private Rigidbody rigidbody;
    private SphereCollider sphereCollider;
    private AudioSource audio;
    public AudioClip killAudio;
    public GameObject explosionEffect;
    public GameObject minimi;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.GameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                TakeHit();
            }
        }
    }
    
    void TakeHit()
    {
        if (currentHealth > 0)
        {
            GameObject newexplosionEffect = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(newexplosionEffect, 1);
            currentHealth -= 10;
        }

        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        sphereCollider.enabled = false;
        audio.PlayOneShot(killAudio);
        rigidbody.isKinematic = true;
        minimi.SetActive(false);
        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


}
