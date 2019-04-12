using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class TargetTracker : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float moveSpeed;
    #endregion

    public Entity stats;
    public string gameObjectToTarget;
    private GameObject targetObject;

    private void Start()
    {
        targetObject = GameObject.Find(gameObjectToTarget);
        moveSpeed = stats.moveSpeed;
    }

    public void Movement()
    {
        if(targetObject != null)
        {
            float step = moveSpeed * Time.deltaTime;

            Vector3 targetDir = targetObject.transform.position - transform.position;
            Vector3 targetPos = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, moveSpeed * Time.deltaTime, 0.0f);

            transform.SetPositionAndRotation(targetPos, Quaternion.LookRotation(newDir));
        }
        else if(targetObject == null)
        {
            print("Target not found.");
            transform.lossyScale.Set(Mathf.Lerp(1, 0, 5), Mathf.Lerp(1, 0, 5), Mathf.Lerp(1, 0, 5));
            transform.Rotate(new Vector3(0, 360 * Time.deltaTime, 0)); // Rotate like a mad man
        }
    }

    private void Update()
    {
        Movement();
    }


}
