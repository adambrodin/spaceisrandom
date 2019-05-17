using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    private Rigidbody rgbd;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rgbd.AddForce(Vector3.back * 10, ForceMode.Acceleration);
    }
}
