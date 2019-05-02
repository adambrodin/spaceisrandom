using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class TargetTracker : MonoBehaviour
{
    #region Variables
    public float movementMulitplier, followMaxDistance, damping;
    protected float moveSpeed;
    public string targetName;
    private GameObject targetObject;
    #endregion

    private void Awake()
    {
        targetObject = GameObject.Find(targetName);
    }

    private void MoveToTarget()
    {
        if (targetObject != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);
            float step = moveSpeed * Time.deltaTime;

            if (distanceToTarget <= followMaxDistance)
            {
                Vector3 targetDir = targetObject.transform.position - transform.position;
                Vector3 targetPos = Vector3.MoveTowards(transform.position, targetObject.transform.position, (step * distanceToTarget) * movementMulitplier);
                Vector3 newDir = Vector3.Lerp(transform.forward, targetDir, damping * Time.deltaTime);

                transform.SetPositionAndRotation(targetPos, Quaternion.LookRotation(newDir));
            }
            else if (Vector3.Distance(transform.position, targetObject.transform.position) > followMaxDistance)
            {
                transform.Translate(Vector3.forward);
            }
        }
        else
        {
            if (Debug.isDebugBuild) Debug.LogWarning("No target found");
            GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
            foreach(GameObject g in objects)
            {
                Destroy(g);
            }
        }
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }


}
