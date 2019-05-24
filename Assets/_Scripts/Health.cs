using System;
using System.Collections;
using UnityEngine;
#pragma warning disable CS0649 // Disable incorrect warnings in the console caused by private variables with [SerializeField]
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

    public event Action<GameObject> OnObjectKilled;
    public event Action<float> OnPlayerHit;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private float blinkTime;
    private EntityBase entity;
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
            entity = GetComponent<EntityBase>();
            StartHealth = entity.stats.startHealth;
            CurrentHealth = StartHealth;
            KillReward = entity.stats.killReward;
        }
        catch (Exception) { return; }

        OnObjectKilled += GameController.Instance.ObjectKilled;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (gameObject.tag == "Player") { OnPlayerHit?.Invoke(CurrentHealth); }
        StartCoroutine(ColorBlink());
        if (IsDead()) { Die(); }
    }

    public IEnumerator ColorBlink()
    {
        for (int rend = 0; rend < entity.renderers.Length; rend++)
        {
            for (int mat = 0; mat < entity.renderers[rend].materials.Length; mat++)
            {
                entity.renderers[rend].materials[mat].SetColor("_BaseColor", InvertColor(entity.renderers[rend].materials[mat].GetColor("_BaseColor")));
                entity.renderers[rend].materials[mat].SetColor("_EmissionColor", InvertColor(entity.renderers[rend].materials[mat].GetColor("_EmissionColor")));
            }
        }
        yield return new WaitForSeconds(blinkTime);
        for (int i = 0; i < entity.originalMaterials.Count; i++) { entity.renderers[i].materials = entity.originalMaterials[i]; }
    }

    public void Die()
    {
        SpawnExplosion();
        OnObjectKilled?.Invoke(gameObject);
    }

    private void SpawnExplosion()
    {
        if (explosion != null)
        {
            GameObject g = Instantiate(explosion, transform.position, transform.rotation);
            g.SetActive(true);
            if (g.GetComponent<ParticleSystem>().GetComponent<Renderer>() != null)
            {
                Renderer rend = g.GetComponent<ParticleSystem>().GetComponent<Renderer>();
                entity.originalMaterials.TryGetValue(0, out Material[] parentMaterials);
                rend.material.SetColor("_BaseColor", parentMaterials[0].GetColor("_BaseColor"));
                rend.material.SetColor("_EmissionColor", parentMaterials[0].GetColor("_EmissionColor"));
            }
            else if (Debug.isDebugBuild) { print("Explosion renderer not found."); }
            FindObjectOfType<AudioManager>().SetPlaying("Explosion", true);
        }
    }
    public bool IsDead() => CurrentHealth <= 0;

    // Make sure the values remain the same whenever they are changed
    private void OnValidate()
    {
        StartHealth = startHealth;
        CurrentHealth = currentHealth;
    }

    private Color InvertColor(Color orgColor)
    {
        // Return the opposite color (color wheel)
        Color.RGBToHSV(orgColor, out float H, out float S, out float V);
        H -= 0.5f;
        return Color.HSVToRGB(H, S, V);
    }
}
