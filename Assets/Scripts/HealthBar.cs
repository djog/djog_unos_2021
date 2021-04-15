using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject bar;
    public float maxHealth;
    public float health;

    void Update()
    {
        float healthPercentage = health / maxHealth;
        bar.transform.localScale = new Vector3(healthPercentage, 1f);
    }
}
