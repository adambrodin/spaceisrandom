/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class TargetTracker : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected float moveSpeed, step, distanceToTarget;
    [SerializeField]
    protected string targetName;
    protected Vector3 targetDir, newPos, newDir;
    protected Rigidbody rgbd, targetRgbd;
    #endregion

    protected virtual void Awake()
    {
        try
        {
            rgbd = GetComponent<Rigidbody>();
            targetRgbd = GameObject.Find(targetName).GetComponent<Rigidbody>();
        }
        catch (Exception) { Destroy(this); }
    }

    protected abstract void CalculateMovement();
    protected virtual void FixedUpdate()
    {
        if (rgbd != null)
        {
            // Calculate distance to target, movement speed and movement path
            if (targetRgbd != null) { distanceToTarget = Vector3.Distance(rgbd.position, targetRgbd.position); }
            step = moveSpeed * Time.deltaTime;
            CalculateMovement();

            // Move and rotate the object
            if (newPos != Vector3.zero) { rgbd.position = Vector3.MoveTowards(rgbd.position, newPos, step); }
            if (newDir != Vector3.zero) { rgbd.rotation = Quaternion.LookRotation(newDir); }
        }
    }
}
