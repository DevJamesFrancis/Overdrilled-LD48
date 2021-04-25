using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrePickupDetector : MonoBehaviour {
    
    public GameObject oreToPickup;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "GoldOre" || other.gameObject.tag == "DiamondOre" || other.gameObject.tag == "WaterOre" ) {
            oreToPickup = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        oreToPickup = null;
    }
}
