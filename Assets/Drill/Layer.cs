using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour {
    
    private GameEngine engine;
    private LayerSpawner spawner;
    [SerializeField] private Vector2[] spawnLocations;

    private void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
        spawner = GameObject.FindGameObjectWithTag("LayerSpawner").GetComponent<LayerSpawner>();
    }

    private void Update() {
        if (!engine.isAlive) {
            return;
        }
        
        float adjustedY = transform.position.y + (engine.currentDrillSpeed * Time.deltaTime);
        transform.position = new Vector2(transform.position.x, adjustedY);

        CheckDeathPosition();
    }

    private void CheckDeathPosition() {
        if (transform.localPosition.y >= 12) {
            spawner.SpawnNewLayer();
            Destroy(this.gameObject);
        }
    }

    public void SetupLayer(List<GameObject> materials) {
        for (int i = 0; i < materials.Count; i++) {
            GameObject material = materials[i];
            material.transform.SetParent(this.transform);
            material.transform.localPosition = spawnLocations[i];
        }
    }
}
