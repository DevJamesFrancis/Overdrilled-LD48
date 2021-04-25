using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour {
    
    private GameEngine engine;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] tank_sprites; 
 
    void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }

    void Update() {
        if (!engine.isAlive) {
            return;
        }

        UpdateWaterTankSprite();
    }

    private void UpdateWaterTankSprite() {
        float waterRatio = engine.currentWater / engine.maxWater;
        if (waterRatio <= 0.05) {
            spriteRenderer.sprite = tank_sprites[0];
            return;
        }
        if (waterRatio <= 0.2) {
            spriteRenderer.sprite = tank_sprites[1];
            return;
        }
        if (waterRatio <= 0.4) {
            spriteRenderer.sprite = tank_sprites[2];
            return;
        }
        if (waterRatio <= 0.6) {
            spriteRenderer.sprite = tank_sprites[3];
            return;
        }
        if (waterRatio <= 0.8) {
            spriteRenderer.sprite = tank_sprites[4];
            return;
        }
        spriteRenderer.sprite = tank_sprites[5];
    }
}
