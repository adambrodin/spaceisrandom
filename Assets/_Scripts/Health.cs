using System;
using UnityEngine;

public class Health : MonoBehaviour, IKillable<float>
{
    #region Variables
    public float StartHealth { get => startHealth; set => startHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    public static event Action<GameObject> entityKilled;
    private float startHealth, currentHealth;
    public Entity stats;
    #endregion

    private void Start()
    {
        try
        {
            StartHealth = stats.startHealth;
            CurrentHealth = StartHealth;
        }
        catch (Exception e)
        {
            print("Exception: " + e.Data);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (IsDead())
        {
            Die();
        }
    }

    public void Die()
    {
        entityKilled?.Invoke(gameObject);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    // Make sure the values remain the same whenever they are changed
    private void OnValidate()
    {
        StartHealth = StartHealth;
        CurrentHealth = currentHealth;
    }
}
