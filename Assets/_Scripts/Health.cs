using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour, IKillable
{
    #region Variables
    public Entity stats;
    private Entity.Type thisType;
    private float damage;
    private bool isPlayer;
    private MeshRenderer rend;

    private Color[] defaultColor;
    public Color blinkColor;

    private TextMeshProUGUI scoreText; // TODO IMPLEMENT BETTER

    public float startHealth { get; set; }
    public float currentHealth { get; set; }
    #endregion

    private void Awake()
    {
        rend = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        currentHealth = stats.startHealth; // Set the current health to the starting health at object creation
        thisType = stats.type;
        if(thisType == Entity.Type.Player)
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

        defaultColor = new Color[rend.materials.Length];
        for(int i = 0; i < defaultColor.Length; i++)
        {
            defaultColor[i] = UnityEngine.Random.ColorHSV();
            rend.materials[i].SetColor("_BaseColor", defaultColor[i]);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(thisType == Entity.Type.Player || thisType == Entity.Type.Enemy)
        {
            try
            {
                damage = col.gameObject.GetComponent<Player_Bullet_Default>().bullet.projectileDamage;
            }
            catch(Exception) { }

            switch(col.gameObject.tag)
            {
                case "EnemyBullet":
                    if(isPlayer)
                    {
                        TakeDamage(damage);
                        Destroy(col.gameObject);
                    }
                    break;
                case "PlayerBullet":
                    if(!isPlayer)
                    {
                        TakeDamage(damage);
                        Destroy(col.gameObject);
                    }
                    break;
                case "Enemy":
                    if(isPlayer)
                    {
                        TakeDamage(1);
                        print("Player hit by Enemy. Current Health: " + currentHealth);
                    }
                    break;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die(); // Dead
        }

        StartCoroutine(blinkEffect());
    }

    private IEnumerator blinkEffect()
    {
        Color startColor = rend.materials[0].GetColor("_BaseColor");
        Color onHitColor = Color.Lerp(startColor, blinkColor, 1f);

        for(int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].SetColor("_BaseColor", onHitColor);
        }

        yield return new WaitForSeconds(stats.blinkTime);

        for(int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].SetColor("_BaseColor", defaultColor[i]);
        }
    }

    public void Die()
    {
        if(isPlayer)
        {
            Destroy(this.gameObject);
            // TODO player death
        }
        else // If not a player -> a enemy
        {
            Destroy(this.gameObject);
            int i = int.Parse(scoreText.text);
            int ii = i += 1000;
            scoreText.text = ii.ToString();
        }
    }
}
