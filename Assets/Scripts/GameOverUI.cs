using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;
        Hide();
    }

    private void GameManagerOnStateChanged() {
        if (GameManager.Instance.CurrentState == GameManager.State.GameOver) {
            Show();
        } else {
            Hide();
        }
    }

    public void OnRestartButtonClicked() {
        GameManager.Instance.Restart();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
