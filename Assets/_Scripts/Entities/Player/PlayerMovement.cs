using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region Variables
    public float tiltValue;
    public float MoveSpeed { get; set; }

    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    #endregion

    private void Start()
    {
        MoveSpeed = GetComponent<Player>().stats.moveSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            Rgbd.velocity.y,
            Input.GetAxis("Vertical"));

        Rgbd.velocity = movement * MoveSpeed;
        Rgbd.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rgbd.velocity.x * -tiltValue);
    }
}
