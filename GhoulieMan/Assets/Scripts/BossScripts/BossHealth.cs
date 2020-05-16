using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int bossHealth = 20;
    private BossController bossController;
    private Animator anim;
    private BoxCollider swordTrigger;
    public bool bossDead = false;
    private CapsuleCollider capsuleCollider;
    private SphereCollider triggerCollider;

    public Material hurtBossMaterial;
    public Material bossMaterial;
    private GameObject bossModel;

    public AudioClip newTrack;
    public AudioClip hitBoss;
    public AudioClip dieBoss;
    private AudioManager audioManager;
    private AudioSource audioBoss;
    private GameObject rockPS;
    private GameObject pause;
    private CharacterMovement characterMovement;
    public GameObject videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GameObject.Find("Video Player");
        videoPlayer.SetActive(false);
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        anim = GameObject.Find("Boss").GetComponent<Animator>();
        swordTrigger = GameObject.Find("Boss").GetComponentInChildren<BoxCollider>();
        triggerCollider = GameObject.Find("Boss").GetComponentInChildren<SphereCollider>();
        capsuleCollider = GameObject.Find("Boss").GetComponent<CapsuleCollider>();
        rockPS = GameObject.Find("RockPS");
        pause = GameObject.Find("Pause");
        bossModel = GameObject.FindGameObjectWithTag("BossModel");
        bossModel.GetComponent<SkinnedMeshRenderer>().material = bossMaterial;
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioBoss = GameObject.Find("Boss").GetComponent<AudioSource>();
        characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon" && bossHealth > 0 && bossController.bossAwake)
        {
            swordTrigger.enabled = false;
            anim.SetTrigger("isHit");
            bossHealth--;
            audioBoss.PlayOneShot(hitBoss);
            //print("Boss Health " + bossHealth);
            ChangeMaterial();
        }
        else if(bossHealth <= 0)
        {
            BossDead();
        }
    }

    void BossDead()
    {
        bossDead = true;
        anim.SetTrigger("isDead");
        audioBoss.PlayOneShot(dieBoss);
        bossController.bossAwake = false;
        swordTrigger.enabled = false;
        capsuleCollider.enabled = false;
        triggerCollider.enabled = false;
        rockPS.SetActive(false);

        if (newTrack != null)
        {
            audioManager.ChangeMusicOneShot(newTrack);
        }
        StartCoroutine(PlayVideo());
    }

    public void ChangeMaterial()
    {
        if (bossHealth < 6)
        {
            bossModel.GetComponent<SkinnedMeshRenderer>().material = hurtBossMaterial;
        }else
        {
            bossModel.GetComponent<SkinnedMeshRenderer>().material = bossMaterial;
        }
    }

    IEnumerator PlayVideo()
    {
        yield return new WaitForSeconds(5);
        characterMovement.enabled = false;
        pause.SetActive(false);
        videoPlayer.SetActive(true);
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(0);
    }
}
