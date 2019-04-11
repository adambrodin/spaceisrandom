using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Variables
    public Entity stats;
    private Entity.Type thisType;
    private float damage, currentHealth;
    private MeshRenderer rend;

    private Color[] defaultColor;
    #endregion

    private void Awake()
    {
        rend = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        currentHealth = stats.startHealth; // Set the current health to the starting health at object creation
        thisType = stats.type;

        defaultColor = new Color[rend.materials.Length];
        for(int i = 0; i < defaultColor.Length; i++)
        {
            defaultColor[i] = UnityEngine.Random.ColorHSV();
            rend.materials[i].SetColor("_BaseColor", defaultColor[i]);

            print("Random color: " + defaultColor[i].ToString());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(thisType == Entity.Type.Player || thisType == Entity.Type.Enemy)
        {
            try
            {
                damage = collision.gameObject.GetComponent<BulletBehaviour>().bullet.projectileDamage;
            }
            catch(Exception e)
            {
                print(e.Data);
            }

            switch(collision.gameObject.tag)
            {
                case "EnemyBullet":
                    if(thisType == Entity.Type.Player)
                    {
                        takeDamage(damage);
                        Destroy(collision.gameObject);
                    }
                    break;
                case "PlayerBullet":
                    if(thisType == Entity.Type.Enemy)
                    {
                        takeDamage(damage);
                        Destroy(collision.gameObject);
                    }
                    break;
            }
        }
    }

    private void takeDamage(float damage)
    {
        currentHealth -= damage;

        print("Took damage");

        if(currentHealth <= 0)
        {
            Destroy(gameObject); // Self destruct
        }

        StartCoroutine(blinkEffect());
    }

    private IEnumerator blinkEffect()
    {
        Color onHitColor = Color.Lerp(Color.black, Color.white, 1f);

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
}
