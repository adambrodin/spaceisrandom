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
    #endregion

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        currentHealth = stats.startHealth; // Set the current health to the starting health at object creation
        thisType = stats.type;
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

        StartCoroutine(blinkEffect());
    }

    private IEnumerator blinkEffect()
    {
        Color startColor = rend.material.color;

        for(int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].color = stats.onHitColor;
        }
        //rend.material.color = stats.onHitColor;

        yield return new WaitForSeconds(stats.blinkTime);

        for(int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].color = startColor;
        }

        //rend.material.color = startColor;

    }
}
