using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour, IMoveable
{
    public Entity stats;

    public Rigidbody rgbd => GetComponent<Rigidbody>();

    public float MoveSpeed { get; set; }

    public abstract void Move();
}
