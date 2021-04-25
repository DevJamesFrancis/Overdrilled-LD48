using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItem : MonoBehaviour {
    
    private GameEngine engine;
    [SerializeField] private bool isOnConveyor = true;

    private void Start() {
        engine = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<GameEngine>();
    }

    private void Update() {
        if (!engine.isAlive) {
            return;
        }
        
        if (isOnConveyor) {
            float adjustedX = transform.position.x - (engine.currentConveyorSpeed * Time.deltaTime);
            transform.position = new Vector2(adjustedX, transform.position.y);

            CheckDeathPosition();
        }
    }

    private void CheckDeathPosition() {
        if (transform.localPosition.x <= -9.25) {
            Destroy(this.gameObject);
        }
    }

    public void RemoveFromConveyor() {
        isOnConveyor = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
