using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillMovement : MonoBehaviour {

    private Inputs inputs;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameEngine engine;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 moveValue;

    #region InputSystem
    private void Awake() { inputs = new Inputs(); }
    #endregion
    
    #region Lifecycle
    private void Update() {
        if (!engine.isAlive) {
            return;
        }

        if (CanMove()) {
            moveValue = GetMovementInput();
        }

        if (CanInteract()) {
            inputs.Drill.Interact.performed += context => {
                PerformInteraction();
            };
        }
    }

    private void FixedUpdate() {
        if (!engine.isAlive) {
            return;
        }
        
        rb.velocity = new Vector2(moveValue.x * moveSpeed, moveValue.y * moveSpeed);
    }
    #endregion

    #region Movement
    private bool _canMove = true;
    private bool CanMove() {
        return _canMove;
    }

    private Vector2 GetMovementInput() {
        return inputs.Drill.Movement.ReadValue<Vector2>();
    }
    #endregion
    
    #region Interaction
    private bool _canInteract = true;
    private bool CanInteract() {
        return _canInteract;
    }

    private void PerformInteraction() {
        engine.ProcessDrillInteraction();
    }
    #endregion
    
    #region Controls Toggling
    public void SetDrillControlsActive(bool controlsActive) {
        if (controlsActive) {
            inputs.Drill.Enable();
            return;
        }
        inputs.Drill.Disable();
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "GoldMaterial") {
            engine.GoldCollision(true);
            return;
        }
        if (other.gameObject.tag == "DiamondMaterial") {
            engine.DiamondCollision(true);
            return;
        }
        if (other.gameObject.tag == "WaterMaterial") {
            engine.WaterCollision(true);
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "GoldMaterial") {
            engine.GoldCollision(false);
            return;
        }
        if (other.gameObject.tag == "DiamondMaterial") {
            engine.DiamondCollision(false);
            return;
        }
        if (other.gameObject.tag == "WaterMaterial") {
            engine.WaterCollision(false);
            return;
        }
    } 
    #endregion
}
