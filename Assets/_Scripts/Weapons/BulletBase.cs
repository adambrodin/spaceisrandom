using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBase : MonoBehaviour, IMoveable
{
    #region Variables
    public Bullet stats;
    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    public string[] targetTags;

    [SerializeField]
    public float MoveSpeed { get => stats.projectileSpeed; set => stats.projectileSpeed = value; }
    #endregion

    private void OnTriggerEnter(Collider col)
    {
        try
        {
            Health h = col.GetComponent<Health>();

            if (IsTargetTag(col.gameObject))
            {
                h.TakeDamage(stats.projectileDamage);

                Destroy(gameObject); // Destroy the bullet after impact
            }
        }
        catch (Exception) { }
    }

    private bool IsTargetTag(GameObject obj)
    {
        foreach (string tag in targetTags)
        {
            if (obj.gameObject.tag == tag)
            {
                return true;
            }
        }

        return false; // If no tag similarity is found
    }

    public void Move()
    {
        // Rgbd.AddForce(Vector3.forward * MoveSpeed, ForceMode.VelocityChange);
        Rgbd.velocity = Vector3.forward * MoveSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Destroy self when out of rendered area
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
