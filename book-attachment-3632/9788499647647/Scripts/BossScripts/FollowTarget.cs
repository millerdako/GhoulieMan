using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("HeadTransform").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate (){
		TrackTarget ();
	}

	void TrackTarget (){
		float targetX = target.position.x;
		float targetY = target.position.y;

		transform.position = new Vector2 (targetX, targetY);
	}


}
