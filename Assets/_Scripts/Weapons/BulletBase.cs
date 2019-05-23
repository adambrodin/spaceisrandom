using System;
using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
[RequireComponent(typeof(Rigidbody))]
public class BulletBase : MonoBehaviour, IMoveable
{
    #region Variables
    [SerializeField]
    protected Bullet stats;
    [HideInInspector]
    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    [SerializeField]
    protected string[] targetTags;

    public float MoveSpeed { get => stats.projectileSpeed; set => stats.projectileSpeed = value; }
    [SerializeField]
    protected float selfDestructTime;
    [SerializeField]
    protected bool useAcceleration;
    #endregion

    // Destroy self automatically after selfDestructTime seconds
    private void Start() => Invoke("OnBecameInvisible", selfDestructTime);
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<IKillable<float>>() != null && IsTargetTag(col.gameObject.tag))
        {
            col.GetComponent<IKillable<float>>().TakeDamage(stats.projectileDamage);
            // Destroy the bullet after impact
            Destroy(gameObject);
        }
        return;
    }

    private bool IsTargetTag(String targetTag)
    {
        foreach (string tag in targetTags)
        {
            if (tag == targetTag)
            {
                return true;
            }
        }

        // If no tag similarity is found
        return false;
    }

    public void Move()
    {
        if (useAcceleration) { Rgbd.AddForce(transform.forward * MoveSpeed, ForceMode.VelocityChange); }
        else { Rgbd.velocity += transform.forward * MoveSpeed; }
    }
    private void FixedUpdate() => Move();

    // Destroy self when out of rendered area
    private void OnBecameInvisible() => Destroy(gameObject);
}
