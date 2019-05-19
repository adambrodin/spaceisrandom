using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    private Rigidbody rgbd;
    private float moveSpeed;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        moveSpeed = GetComponentInChildren<EntityBase>().stats.moveSpeed;
    }

    private void FixedUpdate()
    {
        rgbd.AddForce(Vector3.back * moveSpeed, ForceMode.Acceleration);
    }
}
