using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy03Move : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private Enemy03Health enemy03Health;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemy03Health = GetComponent<Enemy03Health> ();
    }

    // Update is called once per frame
    void Update()
    {
      if (Vector3.Distance (player.position, this.transform.position) < 12) {
        if (!GameManager.instance.GameOver && enemy03Health.IsAlive) {
          nav.SetDestination (player.position);
          anim.SetBool ("isWalking", true);
          anim.SetBool ("isIdle", false);
        }else if (GameManager.instance.GameOver || !enemy03Health.IsAlive) {
        anim.SetBool ("isWalking", false);
        anim.SetBool ("isIdle", true);
        nav.enabled = false;
        }
      }
    }
}
