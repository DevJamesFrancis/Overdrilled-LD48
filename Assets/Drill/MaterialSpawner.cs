using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSpawner : MonoBehaviour {

    [SerializeField] private GameObject[] materials;

    public List<GameObject> SpawnMaterials(int specialSpawn) {
        List<GameObject> spawned = new List<GameObject>();

        // Getting a random column for the special spawn.
        int specialSpawnColumn = Random.Range(0,4);

        for (int i = 0; i < 4; i++) {
            // Checking and substituting in the special spawn.
            int objectToSpawnIndex = 0;
            if (specialSpawn > 0 && specialSpawnColumn == i) {
                objectToSpawnIndex = specialSpawn;
            }

            GameObject spawnObject = materials[objectToSpawnIndex];
            GameObject spawnedObject = Instantiate(spawnObject);
            spawnedObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            spawned.Add(spawnedObject);
        }

        return spawned;
    }
}
