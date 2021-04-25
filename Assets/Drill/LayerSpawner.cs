using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSpawner : MonoBehaviour {
    
    [SerializeField] private GameEngine engine;
    [SerializeField] private MaterialSpawner spawner;
    [SerializeField] private GameObject layer;

    private void Start() {
        // Setup the initial layers.
        for (int i = 0; i < 12; i++) {
            GameObject newLayer = GenerateLayer(0);
            newLayer.transform.localPosition = new Vector2(0, i);
        }
    }

    // When a layer reaches it's death point, it calls this.
    public void SpawnNewLayer() {
        engine.IncreaseDepthValue();
        DoSpawn();
    }

    private void DoSpawn() {
        int specialSpawn = CheckSpecialSpawnLayer();
        GenerateLayer(specialSpawn);
    }

    private GameObject GenerateLayer(int specialSpawnIndex) {
        List<GameObject> materials = spawner.SpawnMaterials(specialSpawnIndex);

        GameObject newLayer = Instantiate(layer, this.transform);
        newLayer.transform.localPosition = new Vector2();
        newLayer.GetComponent<Layer>().SetupLayer(materials);
        return newLayer;
    }

    private int CheckSpecialSpawnLayer() {
        if (engine.depth % 4 == 0) {
            return 1; // Gold
        }
        if (engine.depth % 5 == 0) {
            return 2; // Diamond
        }
        if (engine.depth % 2 == 0) {
            return 3; // Water
        }
        return 0; // Dirt
    }
}
