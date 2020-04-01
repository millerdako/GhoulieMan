using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemScript : MonoBehaviour
{
    private GameObject player;
    private AudioSource audio;
    private ParticleSystem particleSystem;
    private PlayerHealth playerHealth;
    private MeshRenderer meshRenderer;
    private ParticleSystem brainParticles;
    public GameObject pickupEffect;
    private ItemExplode itemExplode;
    private SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth> ();
        playerHealth.enabled = true;
        particleSystem = player.GetComponent<ParticleSystem> ();
        particleSystem.enableEmission = false;
        meshRenderer = GetComponentInChildren <MeshRenderer> ();
        brainParticles = GetComponent<ParticleSystem> ();
        itemExplode = GetComponent<ItemExplode> ();
        sphereCollider = GetComponent<SphereCollider> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject == player){
            StartCoroutine (InvencibleRoutine());
            meshRenderer.enabled = false;
        }
    }

    public IEnumerator InvencibleRoutine () {
        print ("pick PowerItem");
        itemExplode.PickUp();
        particleSystem.enableEmission = true;
        playerHealth.enabled = false;
        brainParticles.enableEmission = false;
        sphereCollider.enabled = false;
        yield return new WaitForSeconds (10f);
        print ("no more invencible");
        particleSystem.enableEmission = false;
        playerHealth.enabled = true;
        Destroy (gameObject);
    }

    /*void Pickup (){
        print ("uno");
        Instantiate (pickupEffect, transform.position, transform.rotation);
    }*/
}
