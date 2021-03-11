using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.name.Contains("Player"))
    {
      var enemies = FindObjectsOfType<EnemyAI>();
      var numberOfEnemies = enemies.Length;
      if (numberOfEnemies == 0)
      {
        Destroy(gameObject);

      }
    }
  }
}
