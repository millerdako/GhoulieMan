using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExplode : MonoBehaviour
{
    public GameObject pickupEffect;

    public void PickUp(){
        GameObject newPickupEffect = (GameObject)Instantiate (pickupEffect, transform.position, transform.rotation);
        Destroy (newPickupEffect, 1);
    }
}
