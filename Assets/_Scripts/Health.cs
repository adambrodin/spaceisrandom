using System;
using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class Health : MonoBehaviour, IKillable<float>
{
    #region Variables
    public float StartHealth { get => startHealth; set => startHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    public static event Action<GameObject> entityKilled;
    private float startHealth, currentHealth;
    public Entity stats;

    public OnHitEffect onHitEffect;
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

        OnTookDamage();

        if (IsDead())
        {
            Die();
        }
    }

    public void OnTookDamage()
    {
        switch (onHitEffect.effectType)
        {
            case OnHitEffect.EffectType.colorBlink:
                StartCoroutine(ColorBlink(onHitEffect.blinkColor, onHitEffect.blinkTime));
                break;
            default:
                print("No OnHitEffect found.");
                break;
        }
    }

    public IEnumerator ColorBlink(Color blinkCol, float blinkTime)
    {
        Color[] orgColors = new Color[GetComponentInChildren<MeshRenderer>().materials.Length];

        // Fetch all colors in all materials from the MeshRenderer
        for (int i = 0; i < orgColors.Length; i++)
        {
            orgColors[i] = GetComponentInChildren<MeshRenderer>().materials[i].color;
        }

        try
        {
            foreach (Material m in GetComponentInChildren<MeshRenderer>().materials)
            {
                m.SetColor("_BaseColor", blinkCol);
            }
        }
        catch (Exception e)
        {
            print("Exception: " + e.Data);
            yield break;
        }

        yield return new WaitForSeconds(blinkTime);

        // Set the materials colors back to how they originally were
        for (int i = 0; i < orgColors.Length; i++)
        {
            GetComponentInChildren<MeshRenderer>().materials[i].SetColor("_BaseColor", orgColors[i]);
        }
    }

    public void Die()
    {
        entityKilled?.Invoke(gameObject);
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    // Make sure the values remain the same whenever they are changed
    private void OnValidate()
    {
        StartHealth = StartHealth;
        CurrentHealth = currentHealth;
    }
}
