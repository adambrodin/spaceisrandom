/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using UnityEngine;

public interface IMoveable
{
    float MoveSpeed { get; set; }
    Rigidbody Rgbd { get; }
    void Move();
}
