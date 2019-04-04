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
        Movement();
    }

    public override void Movement()
    {
        rgbd.velocity = new Vector3(Input.GetAxis("Horizontal") * stats.moveSpeed, 0.0f, Input.GetAxis("Vertical") * stats.moveSpeed);
        rgbd.rotation = Quaternion.Euler(0.0f, 0.0f, rgbd.velocity.x * -tiltValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "EnemyBullet":
                break;
        }
    }
}
