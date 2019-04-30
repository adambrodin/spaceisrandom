using System;
using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(MeshRenderer))]
public class Health : MonoBehaviour, IKillable<float>
{
    #region Variables
    public float StartHealth { get => startHealth; set => startHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    public static event Action<GameObject> EntityKilled;

    [SerializeField]
    private float startHealth, currentHealth;

    public EntityEffect[] effects;
    #endregion

    private void Start()
    {
        try
        {
            StartHealth = GetComponent<EntityBase>().getStats().startHealth;
            CurrentHealth = StartHealth;
        }
        catch (Exception e)
        {
            if (Debug.isDebugBuild) print("Exception: " + e.Data);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        PlayEffect(effects[0]);

        if (IsDead())
        {
            Die();
        }
    }

    public void PlayEffect(EntityEffect e)
    {
        if (e != null)
        {
            switch (e.effectType)
            {
                case EntityEffect.EffectType.colorBlink:
                    StartCoroutine(ColorBlink(e.blinkColor, e.blinkTime));
                    break;
                case EntityEffect.EffectType.explosion:
                    break;
                default:
                    break;
            }
        }
    }

    public IEnumerator ColorBlink(Color blinkCol, float blinkTime)
    {
        MeshRenderer meshRen = GetComponentInChildren<MeshRenderer>();

        // Fetch all colors in all materials from the MeshRenderer
        try
        {
            foreach (Material m in meshRen.materials)
            {
                m.SetColor("_BaseColor", blinkCol);
            }
        }
        catch (Exception e)
        {
            if (Debug.isDebugBuild) print("Exception: " + e.Data);
            yield break;
        }

        yield return new WaitForSeconds(blinkTime);

        // Set the materials colors back to how they originally were
        for (int i = 0; i < GetComponent<EntityBase>().entityColors.Length; i++)
        {
            meshRen.materials[i].SetColor("_BaseColor", GetComponent<EntityBase>().entityColors[i]);
        }
    }

    public void Die()
    {
        EntityKilled?.Invoke(gameObject);
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    // Make sure the values remain the same whenever they are changed
    private void OnValidate()
    {
        StartHealth = startHealth;
        CurrentHealth = currentHealth;
    }
}
