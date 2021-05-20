using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject bar;
    public float maxHealth;
    public float health;

    public void UpdateHealth(float percentage)
    {
        bar.transform.localScale = new Vector3(percentage, 1f);
    }
}
