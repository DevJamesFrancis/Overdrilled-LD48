using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

    [SerializeField] private float startDrillSpeed = 0.6f;
    [SerializeField] private float startConveyorSpeed = 0.6f;
    [SerializeField] private float goldDrillSpeedModifier = 0.2f;
    [SerializeField] private float diamondDrillSpeedModifier = 0.4f;
    [SerializeField] private float waterDrillSpeedModifier = 0.1f;
    [SerializeField] private float depthSpeedModifier = 0.01f;
    public float currentDrillSpeed;
    public float currentConveyorSpeed;

    public bool isAlive = true;
    public int depth = 0;
    public int score = 0;

    [SerializeField] private int goldScore = 50;
    [SerializeField] private int diamondScore = 75;
    [SerializeField] private int waterScore = 30;

    public float currentDrillHeat = 0f;
    public float drillHeatModifier = 0.01f;
    public float drillHeatGoldModifier = 0.05f;
    public float drillHeatDiamondModifier = 0.1f;
    public float drillHeatWaterModifier = 0f;

    public bool canUpdateWater = true;
    public float currentWater = 30f;
    public float maxWater = 30f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject drill;
    [SerializeField] private ConveyorSpawner conveyorSpawner;
    [SerializeField] private GameObject controlPanelInteractable;
    [SerializeField] private InterfaceScript interfaceScript;

    #region Lifecycle
    void Start() {
        Application.targetFrameRate = 60;
        currentDrillSpeed = startDrillSpeed;
        currentConveyorSpeed = startConveyorSpeed;
        conveyorSpawner = GameObject.FindGameObjectWithTag("ConveyorSpawner").GetComponent<ConveyorSpawner>();
        InvokeRepeating("RepeatUpdateWater", 0.25f, 0.25f);
    }

    void Update() {
        if (isAlive) {
            UpdateDrillSpeed();
            UpdateDrillHeat();
            CheckDeath();
        }
    }
    #endregion

    #region Interactions
    public bool ProcessPlayerInteraction() {
        // Interaction from player to control panel.
        Collider2D controlPanelCollider = controlPanelInteractable.GetComponent<Collider2D>();
        if (controlPanelCollider.bounds.Contains(player.transform.position)) {
            player.GetComponent<PlayerMovement>().SetPlayerControlsActive(false);
            drill.GetComponent<DrillMovement>().SetDrillControlsActive(true);
            return true;
        }

        return false;
    }

    public void ProcessDrillInteraction() {
        drill.GetComponent<DrillMovement>().SetDrillControlsActive(false);
        player.GetComponent<PlayerMovement>().SetPlayerControlsActive(true);
    }
    #endregion

    #region Depth
    public void IncreaseDepthValue() {
        depth += 1;
        conveyorSpawner.Spawn();
        UpdateInterface();
    }
    #endregion

    #region Drill Collisions
    public bool goldCollisionActive = false;
    public bool diamondCollisionActive = false;
    public bool waterCollisionActive = false;

    public void GoldCollision(bool colliding) {
        if (goldCollisionActive != colliding) {
            goldCollisionActive = colliding;
            UpdateDrillSpeed();
        }
    }

    public void DiamondCollision(bool colliding) {
        if (diamondCollisionActive != colliding) {
            diamondCollisionActive = colliding;
            UpdateDrillSpeed();
        }
    }

    public void WaterCollision(bool colliding) {
        if (waterCollisionActive != colliding) {
            waterCollisionActive = colliding;
            UpdateDrillSpeed();
        }
    }

    private void UpdateDrillSpeed() {
        float modifiedSpeed = startDrillSpeed;

        modifiedSpeed += depth * depthSpeedModifier;

        if (goldCollisionActive) {
            modifiedSpeed = modifiedSpeed * goldDrillSpeedModifier;
        } else if (diamondCollisionActive) {
            modifiedSpeed = modifiedSpeed * diamondDrillSpeedModifier;
        } else if (waterCollisionActive) {
            modifiedSpeed = modifiedSpeed * waterDrillSpeedModifier;
        }

        currentDrillSpeed = modifiedSpeed;
        currentConveyorSpeed = currentDrillSpeed;
    }
    #endregion

    #region Drill Heat
    private void UpdateDrillHeat() {
        float modifiedHeat = 0;

        modifiedHeat += Mathf.Clamp(depth * drillHeatModifier, 0, 1);

        if (goldCollisionActive) {
            modifiedHeat += drillHeatGoldModifier;
        } else if (diamondCollisionActive) {
            modifiedHeat += drillHeatDiamondModifier;
        } else if (waterCollisionActive) {
            modifiedHeat = 0;
        }

        currentDrillHeat = modifiedHeat;
    }
    #endregion

    #region Water Supply
    private void RepeatUpdateWater() {
        if (!isAlive) {
            return;
        }

        if (canUpdateWater) {
            UpdateWaterSupply();
        }
    }

    private void UpdateWaterSupply() {
        currentWater -= currentDrillHeat;
    }

    private void CheckDeath() {
        if (currentWater <= 0) {
            isAlive = false;
            interfaceScript.PresentEndGameOverlay(depth, score);
        }
    }

    private void Water_IncreaseSupply() {
        currentWater = Mathf.Clamp(currentWater + waterScore, 0, maxWater);
    }
    #endregion

    #region OreInteractions
    public void DepositGold() {
        bool didDeposit = player.GetComponent<PlayerMovement>().RemoveGoldItem();
        if (didDeposit) {
            Gold_IncreaseScore();
        }
    }

    public void DepositDiamond() {
        bool didDeposit = player.GetComponent<PlayerMovement>().RemoveDiamondItem();
        if (didDeposit) {
            Diamond_IncreaseScore();
        }
    }

    public void DepositWater(){
        bool didDeposit = player.GetComponent<PlayerMovement>().RemoveWaterItem();
        if (didDeposit) {
            Water_IncreaseSupply();
        }
    }
    #endregion

    #region Scoring
    private void Gold_IncreaseScore() {
        score += goldScore;
        UpdateInterface();
    }

    private void Diamond_IncreaseScore() {
        score += diamondScore;
        UpdateInterface();
    }

    private void UpdateInterface() {
        interfaceScript.UpdateDepth(depth);
        interfaceScript.UpdateScore(score);
    }
    #endregion
}
