using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour
{
    public Entity stats;

    public float currentHealth; // Set the current health to the starting health at object creation

    public Rigidbody rgbd;

    public abstract void Movement();

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentHealth = stats.startHealth;
    }
}
