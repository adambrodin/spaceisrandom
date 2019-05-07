using System;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public class TargetTracker : MonoBehaviour
{
    #region Variables
    [SerializeField]
    protected float moveSpeed, step, distanceToTarget, followMaxDistance, damping;
    public string targetName;
    private Rigidbody targetRgbd;
    private Rigidbody Rgbd => GetComponent<Rigidbody>();
    private Vector3 targetDir, targetPos, newDir;
    #endregion

    private void Awake()
    {
        try
        {
            targetRgbd = GameObject.Find(targetName).GetComponent<Rigidbody>();
        }
        catch (Exception)
        {
            Destroy(this);
        }
    }

    private void MoveToTarget()
    {
        distanceToTarget = Vector3.Distance(Rgbd.position, targetRgbd.position);
        step = moveSpeed * Time.deltaTime;

        if (distanceToTarget <= followMaxDistance && distanceToTarget > 25)
        {
            targetDir = targetRgbd.position - Rgbd.position;
            newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
            targetPos = Vector3.MoveTowards(Rgbd.position, targetRgbd.position, step / 2);
        }
        else if (Vector3.Distance(Rgbd.position, targetRgbd.position) > followMaxDistance)
        {
            targetPos = new Vector3(Rgbd.position.x, Rgbd.position.y, GameController.Instance.bounds.zMin -= 10);
            newDir = Vector3.Lerp(Rgbd.position, targetPos, damping * Time.deltaTime);
        }

        Rgbd.position = Vector3.MoveTowards(Rgbd.position, targetPos, step);
        Rgbd.rotation = Quaternion.LookRotation(newDir);
    }

    private void FixedUpdate()
    {
        if (Rgbd != null && targetRgbd != null) MoveToTarget();
    }


}
