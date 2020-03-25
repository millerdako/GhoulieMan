using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

	[SerializeField] private float range = 13f;
	[SerializeField] private float timeBetweenSpawn = 1f;

	private GameObject player;
	private bool playerInRange;

	public Transform enemySpawn;
	public Rigidbody enemyPrefab;

	Rigidbody clone;

	// Use this for initialization
	void Start () {

		enemySpawn = GameObject.Find ("Spawner").transform;
		player = GameManager.instance.Player;

		StartCoroutine (SpawnEnemies ());

		
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			playerInRange = true;
		} else {
			playerInRange = false;
		}

		
	}

	public IEnumerator SpawnEnemies () {

		if (playerInRange && !GameManager.instance.GameOver) {

			clone = Instantiate (enemyPrefab, enemySpawn.position, enemySpawn.rotation) as Rigidbody;
			yield return new WaitForSeconds (timeBetweenSpawn);
		}

		yield return null;
		StartCoroutine (SpawnEnemies ());

	}
}
