using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItemScript : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement playerMovement;
    private ItemExplode itemExplode;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        playerMovement = player.GetComponent<CharacterMovement> ();
        itemExplode = GetComponent<ItemExplode> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject == player){
            playerMovement.PowerUpJump();
            itemExplode.PickUp();
            Destroy (gameObject);
        }
    }
}
