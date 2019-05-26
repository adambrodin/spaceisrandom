using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement_SideToSide : MonoBehaviour, IMoveable
{
    #region Variables
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    private float moveSpeed;
    [HideInInspector]
    public Rigidbody Rgbd => GetComponent<Rigidbody>();

    [SerializeField]
    private float zSpeedMultiplier, minRandomizeTime, maxRandomizeTime;
    private float speedX, speedZ;
    [SerializeField]
    private bool autoRandomizeDirection;
    #endregion

    private void Start()
    {
        MoveSpeed = GetComponent<EntityBase>().stats.moveSpeed;
        speedX = MoveSpeed;
        speedZ = MoveSpeed * zSpeedMultiplier;

        if (autoRandomizeDirection) { StartCoroutine(RandomDirection()); }
    }
    private void FixedUpdate() => Move();
    public void Move()
    {
        // Reverse direction if almost going outside of the screen bounds
        if (Mathf.Abs(transform.position.x - GameController.Instance.bounds.xMax) <= 1 || Mathf.Abs(transform.position.x - GameController.Instance.bounds.xMin) <= 1) { speedX *= -1; }
        Rgbd.velocity = new Vector3(speedX, Rgbd.velocity.y, -speedZ);
    }

    private IEnumerator RandomDirection()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minRandomizeTime, maxRandomizeTime));
        speedX *= -1;
        StartCoroutine(RandomDirection());
    }
}
