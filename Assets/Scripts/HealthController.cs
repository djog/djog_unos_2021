using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FloatEvent : UnityEvent<float> { }


public class HealthController : MonoBehaviour
{
    public bool isPlayer = false;
    [SerializeField] private float startHealth = 100.0f;

    public float Health { get; private set; }
    public bool IsDead { get; private set; }

    [Header("Events")]
    public FloatEvent OnHealthChange;
    public FloatEvent OnHealthScaleChange;
    public UnityEvent OnDeath;

    void Start()
    {
        Health = startHealth;
        HealthChanged();
    }

    void HealthChanged()
    {
        if (isPlayer)
        {
            InGameUI.UpdateText("health", $"{Health} HP");
        }
        OnHealthChange.Invoke(Health);
        OnHealthScaleChange.Invoke(Health / startHealth);
    }

    public void PowerUp(float amount)
    {
        Health -= amount;
    }

    public void DealDamage(float amount)
    {
        if (IsDead)
        {
            return;
        }
        Health -= amount;
        if (Health <= 0)
        {
            OnDeath.Invoke();
            Destroy(gameObject);
            IsDead = true;
            Health = 0; // Make sure health isn't lower then zero (for visual purposes)
        }
        HealthChanged();
    }
}