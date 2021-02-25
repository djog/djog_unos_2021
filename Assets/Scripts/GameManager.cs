using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("There are multiple instances of the GameManager! Make sure there's always exactly one!");
        }
    }
}
