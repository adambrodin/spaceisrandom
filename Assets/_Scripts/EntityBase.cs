using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour
{
    public Entity stats;

    public Rigidbody rgbd;

    public abstract void Movement();

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }
}
