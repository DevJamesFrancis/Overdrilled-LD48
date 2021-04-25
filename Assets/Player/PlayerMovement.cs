using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Inputs inputs;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameEngine engine;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 moveValue;

    [SerializeField] private GameObject holdingOre;
    [SerializeField] private PlayerOrePickupDetector orePickupDetector;
    [SerializeField] private Transform oreHoldLocation;

    #region InputSystem
    private void Awake() { inputs = new Inputs(); }
    private void OnEnable() { inputs.Player.Enable(); }
    private void OnDisable() { inputs.Player.Disable(); }
    #endregion
    
    #region Lifecycle
    private void Start() {
        orePickupDetector = this.GetComponentInChildren<PlayerOrePickupDetector>();
        facingRight = this.transform.localScale;
        facingLeft = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
    }

    private void Update() {
        if (!engine.isAlive) {
            return;
        }
        
        if (CanMove()) {
            moveValue = GetMovementInput();

            if (moveValue.x > 0) {
                FlipPlayerSprite(right: true);
            } else if (moveValue.x < 0) {
                FlipPlayerSprite(right: false);
            }
        }

        if (CanInteract()) {
            inputs.Player.Interact.performed += context => {
                PerformInteraction();
            };
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveValue.x * moveSpeed, moveValue.y * moveSpeed);
    }
    #endregion

    #region Movement
    private bool _canMove = true;
    private bool CanMove() {
        return _canMove;
    }

    private Vector2 GetMovementInput() {
        return inputs.Player.Movement.ReadValue<Vector2>();
    }

    private Vector3 facingRight;
    private Vector3 facingLeft;
    private void FlipPlayerSprite(bool right) {
        if (right) {
            this.transform.localScale = facingRight;
        } else {
            this.transform.localScale = facingLeft;
        }
    }
    #endregion
    
    #region Interaction
    private bool _canInteract = true;
    private bool CanInteract() {
        return _canInteract;
    }

    private void PerformInteraction() {
        if (holdingOre != null) {
            return; // Can't do anything while holding ore.
        }

        if (engine.ProcessPlayerInteraction()) {
            return; // Means we changed controls.
        }

        if (orePickupDetector.oreToPickup != null) {
            PickupOre();
        }
    }

    private void PickupOre() {
        holdingOre = orePickupDetector.oreToPickup;
        holdingOre.GetComponent<ConveyorItem>().RemoveFromConveyor();
        holdingOre.transform.SetParent(oreHoldLocation);
        holdingOre.transform.localPosition = new Vector2(0,0);
    }
    #endregion
    
    #region Controls Toggling
    public void SetPlayerControlsActive(bool controlsActive) {
        if (controlsActive) {
            inputs.Player.Enable();
            return;
        }
        inputs.Player.Disable();
    }
    #endregion

    #region Removing Held Items
    public bool RemoveGoldItem() {
        if (holdingOre != null && holdingOre.tag == "GoldOre") {
            Destroy(holdingOre);
            holdingOre = null;
            return true;
        }
        return false;
    }

    public bool RemoveDiamondItem() {
        if (holdingOre != null && holdingOre.tag == "DiamondOre") {
            Destroy(holdingOre);
            holdingOre = null;
            return true;
        }
        return false;
    }

    public bool RemoveWaterItem() {
        if (holdingOre != null && holdingOre.tag == "WaterOre") {
            Destroy(holdingOre);
            holdingOre = null;
            return true;
        }
        return false;
    }
    #endregion
}
