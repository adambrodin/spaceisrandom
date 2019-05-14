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
    #endregion

    private void Start()
    {
        // Destroy self automatically after selfDestructTime seconds
        Invoke("OnBecameInvisible", selfDestructTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<IKillable<float>>() == null) { return; }
        var killable = col.GetComponent<IKillable<float>>();

        if (IsTargetTag(col.gameObject))
        {
            killable.TakeDamage(stats.projectileDamage);
            // Destroy the bullet after impact
            Destroy(gameObject);
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

        // If no tag similarity is found
        return false;
    }

    public void Move()
    {
        Rgbd.AddRelativeForce(Vector3.forward * MoveSpeed, ForceMode.VelocityChange);
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
