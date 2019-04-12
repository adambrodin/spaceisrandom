using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour, IMoveable
{
    public Entity stats;

    public Rigidbody rgbd;

    public float moveSpeed { get; set; }

    public abstract void Move();

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }
}
