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
    private float step, distanceToTarget;
    protected float moveSpeed;
    public string targetName;
    private GameObject targetObject;
    private Rigidbody rgbd, targetRgbd;
    public bool positionBasedVelocity;
    #endregion

    private void Awake()
    {
        targetObject = GameObject.Find(targetName);
        rgbd = GetComponent<Rigidbody>();
    }

    private void MoveToTarget()
    {
        if (targetObject != null)
        {
            Rigidbody targetRgbd = targetObject.GetComponent<Rigidbody>();

            distanceToTarget = Vector3.Distance(rgbd.position, targetRgbd.position);
            step = moveSpeed * Time.fixedDeltaTime;

            if (distanceToTarget <= followMaxDistance && distanceToTarget > 10)
            {
                Vector3 targetDir = targetRgbd.position - rgbd.position;
                Vector3 targetPos = Vector3.MoveTowards(rgbd.position, targetRgbd.position, step);
                Vector3 newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);

                rgbd.position = targetPos;
                rgbd.rotation = Quaternion.LookRotation(newDir);
            }
            else if (Vector3.Distance(rgbd.position, targetRgbd.position) > followMaxDistance)
            {
                rgbd.velocity = Vector3.back * moveSpeed;
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
