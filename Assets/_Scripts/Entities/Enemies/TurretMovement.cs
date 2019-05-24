using UnityEngine;
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class TurretMovement : TargetTracker
{
    #region Variables
    [SerializeField]
    private float damping, boundryOffset, timeBeforeShoot;
    public float targetZ;
    private bool reachedTargetZ;
    #endregion

    private void Start()
    {
        moveSpeed = GetComponent<EntityBase>().stats.moveSpeed;
        targetZ = UnityEngine.Random.Range(GameController.Instance.bounds.zMin + boundryOffset, GameController.Instance.bounds.zMax - boundryOffset);
    }

    protected override void CalculateMovement()
    {
        // Move downwards to targetZ position
        newPos = new Vector3(rgbd.position.x, rgbd.position.y, targetZ);

        // IF the target has been reached, rotate the turret towards the player

        if (targetRgbd != null)
        {
            if (reachedTargetZ)
            {
                targetDir = targetRgbd.position - rgbd.position;
                newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
            }

            // When the target is first reached
            if (rgbd.position.z == targetZ && !reachedTargetZ)
            {
                reachedTargetZ = true;

                targetDir = targetRgbd.position - rgbd.position;
                newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);

                // Starts the automatic shooting after a small delay
                GetComponent<TurretWeapon>().Invoke("Shoot", timeBeforeShoot);
            }
        }
    }
}
