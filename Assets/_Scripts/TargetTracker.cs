using System;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public abstract class TargetTracker : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected float moveSpeed, step, distanceToTarget;
    [SerializeField]
    protected string targetName;
    protected Vector3 targetDir, targetPos, newDir;
    protected Rigidbody rgbd, targetRgbd;
    #endregion
    
    protected virtual void Awake()
    {
        try
        {
            rgbd = GetComponent<Rigidbody>();
            targetRgbd = GameObject.Find(targetName).GetComponent<Rigidbody>();
        }
        catch (Exception)
        {
            Destroy(this);
        }
    }

    protected abstract void CalculateMovement();

    protected virtual void FixedUpdate()
    {
        if (rgbd != null && targetRgbd != null)
        {
            // Calculate distance to target, movement speed and movement path
            distanceToTarget = Vector3.Distance(rgbd.position, targetRgbd.position);
            step = moveSpeed * Time.deltaTime;
            CalculateMovement();

            // Move the object
            if (targetPos != null) rgbd.position = Vector3.MoveTowards(rgbd.position, targetPos, step);
            if (newDir != null) rgbd.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
