using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPoint : MonoBehaviour
{
    public BoxCollider collider;
    private BossController bossController;
    private CharacterMovement characterMovement;
    private Animator playerAnimator;
    private SmoothFollow smoothFollow;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        smoothFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            collider.isTrigger = false;
            bossController.bossAwake = true;
            characterMovement.enabled = false;
            playerAnimator.Play("Player_Idle");
            smoothFollow.bossCameraActive = true;
        }        
    }
}
