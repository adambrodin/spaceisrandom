using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public class BulletBase : MonoBehaviour, IMoveable
{
    #region Variables
    public Bullet stats;
    [HideInInspector]
    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    public string[] targetTags;

    [SerializeField]
    public float MoveSpeed { get => stats.projectileSpeed; set => stats.projectileSpeed = value; }
    #endregion

    private void OnTriggerEnter(Collider col)
    {
        var k = col.GetComponent<IKillable<float>>();
        {
            if (IsTargetTag(col.gameObject))
            {
                k.TakeDamage(stats.projectileDamage);
                Destroy(gameObject); // Destroy the bullet after impact
            }
        }
        return;
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
        // Rgbd.AddForce(Vector3.forward * MoveSpeed, ForceMode.VelocityChange); TODO maybe maybe
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
