using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class TargetTracker : MonoBehaviour
{
    #region Variables
    public float moveSpeed;
    public string targetName;
    private GameObject targetObject;
    #endregion

    private void Awake()
    {
        targetObject = GameObject.Find(targetName);
    }

    public void MoveToTarget()
    {
        if (targetObject != null)
        {
            float step = moveSpeed * Time.deltaTime;

            Vector3 targetDir = targetObject.transform.position - transform.position;
            Vector3 targetPos = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, moveSpeed * Time.deltaTime, 0.0f);

            transform.SetPositionAndRotation(targetPos, Quaternion.LookRotation(newDir));
        }
        else if (targetObject == null)
        {
            print("Target not found.");
            Destroy(gameObject); // Destroy self
        }
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }


}
