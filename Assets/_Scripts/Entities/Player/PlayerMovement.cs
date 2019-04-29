﻿using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region Variables
    public float tiltValue;
    public float MoveSpeed { get; set; }

    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    private Vector2 direction;
    private Vector3 movement;
    #endregion

    private void Start()
    {
        MoveSpeed = GetComponent<Player>().getStats().moveSpeed;
    }

    private void OnEnable()
    {
        Player.OnGetMovement += OnGetMovement;
    }

    private void OnDisable()
    {
        Player.OnGetMovement -= OnGetMovement;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        movement = new Vector3(direction.x, Rgbd.velocity.y, direction.y);

        Rgbd.velocity = movement * MoveSpeed;
        Rgbd.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rgbd.velocity.x * -tiltValue);
    }

    private void OnGetMovement(Vector2 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Border")
        {
        }
    }
}
