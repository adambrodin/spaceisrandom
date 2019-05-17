using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : TargetTracker
{
    #region Variables
    [SerializeField]
    protected float slowDownDistance, slowSpeedMultiplier, followMaxDistance, damping;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        moveSpeed = GetComponent<EntityBase>().stats.moveSpeed;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (rgbd.position.z <= GameController.Instance.bounds.zMin)
        {
            print("enemy died");
            Destroy(gameObject);
        }
    }

    protected override void CalculateMovement()
    {
        if (distanceToTarget <= followMaxDistance && distanceToTarget > slowDownDistance)
        {
            targetDir = targetRgbd.position - rgbd.position;
            newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
            targetPos = Vector3.MoveTowards(rgbd.position, targetRgbd.position, step * slowSpeedMultiplier);
        }
        else if (Vector3.Distance(rgbd.position, targetRgbd.position) > followMaxDistance)
        {
            targetPos = new Vector3(rgbd.position.x, rgbd.position.y, GameController.Instance.bounds.zMin -= rgbd.position.z);
            newDir = Vector3.Lerp(rgbd.position, targetPos, damping * Time.deltaTime);
        }
    }
}


