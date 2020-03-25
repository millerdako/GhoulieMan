using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMovement : MonoBehaviour {

	[SerializeField] Vector3 leftPosition;
	[SerializeField] Vector3 rightPosition;
	[SerializeField] float speed;

	// Use this for initialization
	void Start () {

		StartCoroutine (Move (rightPosition));

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Move (Vector3 target) {

		while (Mathf.Abs ((target - transform.localPosition).x) > 0.20f) {

			Vector3 direction = target.x == leftPosition.x ? Vector3.left : Vector3.right;
			transform.localPosition += direction * Time.deltaTime * speed;

			yield return null;
		}

		yield return new WaitForSeconds (0.5f);

		Vector3 newTarget = target.x == leftPosition.x ? rightPosition : leftPosition;

		StartCoroutine (Move (newTarget));

	}
}
