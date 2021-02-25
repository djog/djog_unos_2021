using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are multiple instances of the GameManager! Make sure there's always exactly one!");
        }
    }

    public UIController uiController;

    [Header("Player")]
    public Transform playerSpawn;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    [Header("Enemies")]
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;

    private GameObject player;

    void Start()
    {
        // Spawn the player
        player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        var camera = Instantiate(cameraPrefab, playerSpawn.position, playerSpawn.rotation);
        camera.GetComponent<SmoothCameraFollow2D>().target = player.transform;
        camera.GetComponent<SmoothCameraFollow2D>().rigidbody = player.GetComponent<Rigidbody2D>();
        player.GetComponent<HealthController>().OnDeath.AddListener(GameOver);

        // Spawn the enemies
        foreach (var spawnpoint in enemySpawnPoints)
        {
            var enemy = Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
        }
    }

    void GameOver() {
        uiController.OpenGameoverScreen();
    }

    // Get all the target (for the AI)
    public static Transform GetTarget()
    {
        return instance.player.transform;
    }
}
