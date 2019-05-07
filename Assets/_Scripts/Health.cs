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
    [SerializeField]
    private float startHealth, currentHealth;
    public event Action<GameObject> EntityKilled;
    [SerializeField]
    private EntityEffect[] effects;

    private static Health instance;
    #endregion
    public static Health Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(Health)) as Health;
            }
            return instance;
        }
    }

    private void Start()
    {
        if (GetComponent<EntityBase>() != null) StartHealth = GetComponent<EntityBase>().getStats().startHealth;
        CurrentHealth = StartHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (effects.Length > 0) PlayEffect(effects[0]);

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
                    StartCoroutine(ColorBlink(e.blinkTime));
                    break;
                case EntityEffect.EffectType.explosion:
                    break;
            }
        }
    }

    public IEnumerator ColorBlink(float blinkTime)
    {
        MeshRenderer meshRen;
        if (GetComponent<MeshRenderer>() != null)
        {
            meshRen = GetComponent<MeshRenderer>();
        }
        else
        {
            meshRen = GetComponentInChildren<MeshRenderer>();
        }

        Color[] eCol = GetComponent<EntityBase>().entityColors;

        // Fetch all colors in all materials from the MeshRenderer
        try
        {
            for (int i = 0; i < meshRen.materials.Length; i++)
            {
                meshRen.materials[i].SetColor("_BaseColor", InvertColor(meshRen.materials[i].color));
            }
        }
        catch (Exception e)
        {
            if (Debug.isDebugBuild) print("Exception: " + e.Data);
            yield break;
        }

        yield return new WaitForSeconds(blinkTime);

        // Set the materials colors back to how they originally were
        for (int i = 0; i < eCol.Length; i++)
        {
            meshRen.materials[i].SetColor("_BaseColor", eCol[i]);
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

    private Color InvertColor(Color orgColor) ////// TODO
    {
        return Color.white;
    }
}
