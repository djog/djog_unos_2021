using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject inGameParent;
    public GameObject gameOverParent;

    void Start() {
        inGameParent.SetActive(true);
        gameOverParent.SetActive(false);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenGameoverScreen()
    {
        inGameParent.SetActive(false);
        gameOverParent.SetActive(true);
    }
}
