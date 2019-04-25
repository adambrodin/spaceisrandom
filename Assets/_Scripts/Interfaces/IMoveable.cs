using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IMoveable
{
    float MoveSpeed { get; set; }

    Rigidbody rgbd { get; }

    void Move();
}
