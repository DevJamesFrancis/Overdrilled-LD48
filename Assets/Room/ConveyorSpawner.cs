using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSpawner : MonoBehaviour {

    private GameEngine engine;
    [SerializeField] private GameObject[] material_piles;
    
    #region Lifecycle
    private void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }
    #endregion

    #region Spawning
    public void Spawn() {
        GameObject spawnObject = material_piles[SpawnIndex()];
        GameObject spawnedObject = Instantiate(spawnObject, this.transform);
        spawnedObject.transform.localPosition = new Vector2();
    }

    private int SpawnIndex() {
        if (engine.goldCollisionActive) {
            return 1;
        }
        if (engine.diamondCollisionActive) {
            return 2;
        }
        if (engine.waterCollisionActive) {
            return 3;
        }
        return 0;
    }
    #endregion
}
