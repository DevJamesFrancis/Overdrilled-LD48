using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillHeat : MonoBehaviour {

    private GameEngine engine;

    [SerializeField] private GameObject[] drill_shafts;
    [SerializeField] private Sprite[] heat_sprites; 

    void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }

    // Update is called once per frame
    void Update() {
        if (!engine.isAlive) {
            return;
        }
        
        UpdateDrillHeatSprites();
    }

    private void UpdateDrillHeatSprites() {
        Sprite heatSprite = GetDrillHeatSprite();

        foreach (GameObject heatShaft in drill_shafts) {
            heatShaft.GetComponent<SpriteRenderer>().sprite = heatSprite;
        }
    }

    private Sprite GetDrillHeatSprite() {
        if (engine.currentDrillHeat <= 0.1) {
            return heat_sprites[0];
        }
        if (engine.currentDrillHeat <= 0.2) {
            return heat_sprites[1];
        }
        if (engine.currentDrillHeat <= 0.3) {
            return heat_sprites[2];
        }
        return heat_sprites[3];
    }
}
