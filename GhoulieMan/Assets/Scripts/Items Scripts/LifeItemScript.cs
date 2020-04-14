using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItemScript : MonoBehaviour
{
    private GameObject player;
    private AudioSource audio;
    private LifeManager lifeManager;
    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;
    private AudioClip pickItem;
    private ItemExplode itemExplode;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        lifeManager = FindObjectOfType<LifeManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        audio = player.GetComponent<AudioSource>();
        pickItem = player.GetComponent<PlayerHealth>().pickItem;
        itemExplode = GetComponent<ItemExplode>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            PickLife();
            print("Life Collected");
        }
    }
    
    public void PickLife()
    {
        lifeManager.GiveLife();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        audio.PlayOneShot(pickItem);
        itemExplode.PickUp();
        Destroy(gameObject);
    }
    
}
