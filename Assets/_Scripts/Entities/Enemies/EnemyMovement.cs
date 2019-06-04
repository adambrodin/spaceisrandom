/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : TargetTracker
{
    #region Variables
    [SerializeField]
    protected float slowDownDistance, slowSpeedMultiplier, followMaxDistance, damping, boundsOffset, minDistanceToZBoundry;
    [SerializeField]
    protected bool followPlayer;
    #endregion

    private void Start() => moveSpeed = GetComponent<EntityBase>().stats.moveSpeed;
    protected override void CalculateMovement()
    {
        // If the enemy is outside of the screen
        if (rgbd.position.z <= GameController.Instance.bounds.zMin - boundsOffset) { Destroy(gameObject); }
        if (targetRgbd != null)
        {
            if (distanceToTarget < followMaxDistance && followPlayer && (Mathf.Abs(transform.position.z - GameController.Instance.bounds.zMin) > minDistanceToZBoundry))
            {
                // Move and rotate towards the player
                targetDir = targetRgbd.position - rgbd.position;
                newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
                float speed = step;
                if (distanceToTarget <= slowDownDistance) { speed *= slowSpeedMultiplier; }
                newPos = Vector3.MoveTowards(rgbd.position, targetRgbd.position, speed);
            }
            else
            {
                // Move and rotate straight down
                Vector3 target = new Vector3(rgbd.position.x, rgbd.position.y, GameController.Instance.bounds.zMin - boundsOffset);
                targetDir = target - rgbd.position;
                newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
                newPos = Vector3.MoveTowards(rgbd.position, target, step);
            }
        }
    }
}


