using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOreDropoff : MonoBehaviour {

    private GameEngine engine;

    private void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "GoldChest") {
            engine.DepositGold();
            return;
        } 

        if (other.gameObject.tag == "DiamondChest") {
            engine.DepositDiamond();
            return;
        }

        if (other.gameObject.tag == "WaterChest") {
            engine.DepositWater();
            return;
        }
    }
}
