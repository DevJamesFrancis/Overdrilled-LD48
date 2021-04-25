using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InterfaceScript : MonoBehaviour {
    
    [SerializeField] private GameObject interfacePanel;
    [SerializeField] private TextMeshProUGUI depthText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI endGameDepthText;
    [SerializeField] private TextMeshProUGUI endGameScoreText;

    #region IngameOverlay
    public void UpdateDepth(int depth) {
        string depthString = "Depth: " + depth;
        depthText.text = depthString;
    }

    public void UpdateScore(int score) {
        string scoreString = "Score: " + score;
        scoreText.text = scoreString;
    }
    #endregion

    #region EndGameOverlay
    public void PresentEndGameOverlay(int depth, int score) {
        endGameDepthText.text = "Depth: " + depth;
        endGameScoreText.text = "Score: " + score;
        endGamePanel.SetActive(true);
        interfacePanel.SetActive(false);
    }

    public void RetryPressed() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
