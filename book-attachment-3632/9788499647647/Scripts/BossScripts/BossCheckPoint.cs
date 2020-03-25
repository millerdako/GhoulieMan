using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPoint : MonoBehaviour {

    public BoxCollider collider;
    private BossController bossController;
    private CharacterMovement characterMovement;
    private Animator playerAnimator;

    private SmoothFollow smoothFollow;

    public AudioClip newTrack;
    private AudioManager audioManager;
    public AudioSource audio;
    public AudioClip newTrack03;

    void Awake ()
    {
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        smoothFollow = GameObject.Find ("Main Camera").GetComponent<SmoothFollow>();
    }

	// Use this for initialization
	void Start () {

        collider = GetComponent<BoxCollider>();
        playerAnimator = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        audio = GameObject.FindGameObjectWithTag("Boss").GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            bossController.bossAwake = true;
            collider.isTrigger = false;
            characterMovement.enabled = false;
            playerAnimator.Play("Ghoulie_Idle");
            smoothFollow.bossCameraActive = true;
            
            if (newTrack != null){
                audioManager.ChangeMusic(newTrack);
                audio.PlayOneShot (newTrack03);
            }
        }
    }
}
