using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityBase
{
    #region Variables
    public float tiltValue;
    #endregion
    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        rgbd.velocity = new Vector3(Input.GetAxis("Horizontal") * stats.moveSpeed, 0.0f, Input.GetAxis("Vertical") * stats.moveSpeed);
        rgbd.rotation = Quaternion.Euler(0.0f, 0.0f, rgbd.velocity.x * -tiltValue);

        if(Input.GetKey(KeyCode.Q)) //  TODO CONTROLLER SUPPORT
        {
            float yy = rgbd.velocity.y;
            rgbd.velocity = new Vector3(rgbd.velocity.x, yy += 10, rgbd.velocity.z);
        }

        if(Input.GetKey(KeyCode.E)) //  TODO CONTROLLER SUPPORT
        {
            float yy = rgbd.velocity.y;
            rgbd.velocity = new Vector3(rgbd.velocity.x, yy -= 10, rgbd.velocity.z);
        }
    }
}
