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
            KillReward = entity.stats.killReward;
        }
        catch (Exception) { return; }

        CurrentHealth = StartHealth;
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
                entity.renderers[rend].materials[mat].SetColor("_BaseColor", InvertColor(Color.white));
                entity.renderers[rend].materials[mat].SetColor("_EmissionColor", InvertColor(Color.white));
            }
        }

        yield return new WaitForSeconds(blinkTime);

        for (int rend = 0; rend < entity.renderers.Length; rend++)
        {
            for (int mat = 0; mat < entity.renderers[rend].materials.Length; mat++)
            {
                entity.renderers[rend].materials[mat].SetColor("_BaseColor", entity.renderersInfo[rend].materials[mat].GetColor("_BaseColor"));
                entity.renderers[rend].materials[mat].SetColor("_EmissionColor", entity.renderersInfo[rend].materials[mat].GetColor("_EmissionColor"));
            }
        }
    }

    public void Die()
    {
        //SpawnExplosion();
        OnObjectKilled?.Invoke(gameObject);
    }

    /* private void SpawnExplosion()
    {
        if (explosion != null)
        {
            GameObject g = Instantiate(explosion, transform.position, transform.rotation);
            g.SetActive(true);

            if (g.GetComponent<ParticleSystem>().GetComponent<Renderer>() != null)
            {
                Renderer rend = g.GetComponent<ParticleSystem>().GetComponent<Renderer>();
                if (eCols != null && eChildCols != null)
                {
                    rend.material.SetColor("_BaseColor", eCols[0]);
                    rend.material.SetColor("_EmissionColor", eChildCols[0]);
                }
                else if (eCols != null && eChildCols == null)
                {
                    rend.material.SetColor("_BaseColor", eCols[0]);
                    if (eCols[1] != null) rend.material.SetColor("_EmissionColor", eCols[0]);
                    else { rend.material.SetColor("_EmissionColor", eCols[0]); }
                }
                else if (eCols == null && eChildCols != null)
                {
                    rend.material.SetColor("_BaseColor", eChildCols[0]);
                    if (eChildCols[1] != null) rend.material.SetColor("_EmissionColor", eChildCols[0]);
                    else { rend.material.SetColor("_EmissionColor", eChildCols[0]); }
                }
            }
            else { if (Debug.isDebugBuild) { print("Explosion renderer not found."); } }
            FindObjectOfType<AudioManager>().SetPlaying("Explosion", true);
        }
    } */
    public bool IsDead() => CurrentHealth <= 0;

    // Make sure the values remain the same whenever they are changed
    private void OnValidate()
    {
        StartHealth = startHealth;
        CurrentHealth = currentHealth;
    }

    private Color InvertColor(Color orgColor)
    {
        //Color.RGBToHSV(orgColor, out float H, out float S, out float V);

        //H -= 0.5f;

        //return Color.HSVToRGB(H, S, V);

        // TODO FIX PROPER INVERTION
        return Color.white;
    }
}
