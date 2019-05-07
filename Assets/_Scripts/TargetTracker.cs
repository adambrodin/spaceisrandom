using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public class TargetTracker : MonoBehaviour
{
    #region Variables
    public float followMaxDistance, damping;
    protected float moveSpeed, step, distanceToTarget;
    public string targetName;
    private GameObject targetObject;
    private Rigidbody targetRgbd;
    private Rigidbody Rgbd => GetComponent<Rigidbody>();
    private Vector3 targetDir, targetPos, newDir;
    #endregion

    private void Awake()
    {
        targetObject = GameObject.Find(targetName);
    }

    private void MoveToTarget()
    {
        if (targetObject != null)
        {
            Rigidbody targetRgbd = targetObject.GetComponent<Rigidbody>();

            distanceToTarget = Vector3.Distance(Rgbd.position, targetRgbd.position);
            step = moveSpeed * Time.fixedDeltaTime;

            if (distanceToTarget <= followMaxDistance && distanceToTarget > 10)
            {
                targetDir = targetRgbd.position - Rgbd.position;
                targetPos = Vector3.MoveTowards(Rgbd.position, targetRgbd.position, step / 2);
                newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);

                Rgbd.position = targetPos;
                Rgbd.rotation = Quaternion.LookRotation(newDir);
            }
            else if (Vector3.Distance(Rgbd.position, targetRgbd.position) > followMaxDistance)
            {
                Vector3 targetPos = new Vector3(Rgbd.position.x, Rgbd.position.y, GameController.Instance.bounds.zMin -= 10);
                Rgbd.position = Vector3.MoveTowards(Rgbd.position, targetPos, step);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }


}
