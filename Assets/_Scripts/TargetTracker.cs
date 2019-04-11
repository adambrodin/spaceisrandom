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
    public float moveSpeed;
    #endregion

    public string gameObjectToTarget;
    private GameObject targetObject;

    private void Start()
    {
        targetObject = GameObject.Find(gameObjectToTarget);
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
    }

    private void Update()
    {
        Movement();
    }


}
