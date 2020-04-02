using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemScript : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private ItemExplode itemExplode;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth> ();
        itemExplode = GetComponent<ItemExplode> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject == player){
            playerHealth.PowerUpHealth();
            itemExplode.PickUp();
            Destroy (gameObject);
        }
    }
}
