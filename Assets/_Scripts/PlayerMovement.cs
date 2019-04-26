using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerMovement : EntityBase
{
    #region Variables
    public float tiltValue;
    #endregion

    private void Start()
    {
        RandomizeColors();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            Rgbd.velocity.y,
            Input.GetAxis("Vertical"));

        Rgbd.velocity = movement * MoveSpeed;
        Rgbd.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rgbd.velocity.x * -tiltValue);
    }
}
