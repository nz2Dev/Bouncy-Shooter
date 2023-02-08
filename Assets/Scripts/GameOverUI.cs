using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI message;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;
        Hide();
    }

    private void GameManagerOnStateChanged() {
        if (GameManager.Instance.IsGameFinishedState || GameManager.Instance.IsGameOverState) {
            message.text = GameManager.Instance.IsGameOverState ? "Game Over" : "Game Finished";
            Show();
        } else {
            Hide();
        }
    }

    public void OnOkButtonClicked() {
        Application.Quit();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
