using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBase : UnityEngine.MonoBehaviour, IMoveable
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
        if (col.GetComponent<Health>() != null && IsTargetTag(col.gameObject))
        {
            col.GetComponent<Health>().TakeDamage(stats.projectileDamage);

            Destroy(gameObject); // Destroy the bullet after impact
        }
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
