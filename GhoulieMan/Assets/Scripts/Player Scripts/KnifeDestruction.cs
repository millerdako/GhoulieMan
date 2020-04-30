using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDestruction : MonoBehaviour {

	public float lifeSpan = 2.0f;
    bool destruction = true;

	// Use this for initialization
	void Start () {

		Destroy (gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
        destruction = true;
        if (other.gameObject.GetComponent<KnifeDestruction>() == null)
        {
            if (other.gameObject.GetComponent<BoxCollider>() != null)
            {
                if (other.gameObject.GetComponent<BoxCollider>().isTrigger == true)
                {
                    //print("No se destruye");
                    destruction = false;
                }
            }
            else
            {
                if (other.gameObject.GetComponent<SphereCollider>() != null)
                {
                    if (other.gameObject.GetComponent<SphereCollider>().isTrigger == true)
                    {
                        //print("No se destruye");
                        destruction = false;
                    }
                }

            }
        }      
        
        if (other.gameObject && destruction)
        {
            Destroy(this.gameObject);
        }
	}
}


