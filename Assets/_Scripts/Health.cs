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
    public float KillReward { get => killReward; set => killReward = value; }
    private float startHealth, currentHealth, killReward;

    public event Action<GameObject> EntityKilled;
    public EntityEffect[] effects;

    private MeshRenderer meshRen, childMeshRen;
    private static Health instance;
    #endregion

    public static Health Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType<Health>(); }
            return instance;
        }
    }

    private void Start()
    {
        try
        {
            var stats = GetComponent<EntityBase>().stats;
            StartHealth = stats.startHealth;
            KillReward = stats.killReward;
        }
        catch (Exception) { return; }

        CurrentHealth = StartHealth;
        EntityKilled += GameController.Instance.OnKill;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (effects.Length > 0) { PlayEffect(effects[0]); }
        if (IsDead()) { Die(); }
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
        Color[] eCol = GetComponent<EntityBase>().entityColors;

        if (GetComponent<MeshRenderer>() != null) meshRen = GetComponent<MeshRenderer>();
        if (GetComponentInChildren<MeshRenderer>() != null) childMeshRen = GetComponentInChildren<MeshRenderer>();

        // Fetch all colors in all materials from the MeshRenderer
        if (meshRen != null)
        {
            for (int i = 0; i < meshRen.materials.Length; i++)
            {
                meshRen.materials[i].SetColor("_BaseColor", InvertColor(meshRen.materials[i].color));
            }
        }

        if (childMeshRen != null)
        {
            for (int i = 0; i < childMeshRen.materials.Length; i++)
            {
                childMeshRen.materials[i].SetColor("_BaseColor", InvertColor(childMeshRen.materials[i].color));
            }
        }
        yield return new WaitForSeconds(blinkTime);

        // Set the materials colors back to how they originally were
        for (int i = 0; i < eCol.Length; i++)
        {
            if (meshRen != null) meshRen.materials[i].SetColor("_BaseColor", eCol[i]);
            if (childMeshRen != null) childMeshRen.materials[i].SetColor("_BaseColor", eCol[i]);
        }
    }

    public void Die() => EntityKilled?.Invoke(gameObject);
    public bool IsDead() => CurrentHealth <= 0;

    // Make sure the values remain the same whenever they are changed
    private void OnValidate()
    {
        StartHealth = startHealth;
        CurrentHealth = currentHealth;
    }

    private Color InvertColor(Color orgColor)
    {
        Color.RGBToHSV(orgColor, out float H, out float S, out float V);

        H -= 0.5f;

        //return Color.HSVToRGB(H, S, V);

        // TODO FIX PROPER INVERTION
        return Color.white;
    }
}
