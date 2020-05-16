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
    public AudioClip newTrack;
    private AudioManager audioManager;
    private AudioSource audioBoss;
    public AudioClip enterBoss;

    void Awake ()
    {
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        smoothFollow = GameObject.FindWithTag("MainCamera").GetComponent<SmoothFollow>();
    }
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        audioBoss = GameObject.Find("Boss").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bossController.bossAwake = true;
            collider.isTrigger = false;
            characterMovement.enabled = false;
            playerAnimator.Play("Player_Idle");
            smoothFollow.bossCameraActive = true;
            if (newTrack != null)
            {
                audioManager.ChangeMusic(newTrack);
                audioBoss.PlayOneShot(enterBoss);
            }
        }        
    }
}
