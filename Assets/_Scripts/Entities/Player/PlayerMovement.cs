using UnityEngine;
using System.Collections;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[System.Serializable]
public class Bounds
{
    public float xMin, xMax, zMin, zMax;
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region Variables
    public float tiltValue;
    public float MoveSpeed { get; set; }

    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    public Bounds bounds;
    private Vector2 direction;
    private Vector3 movement;
    #endregion

    private void Start()
    {
        MoveSpeed = GetComponent<Player>().getStats().moveSpeed;
    }

    private void OnEnable()
    {
        Player.Instance.OnGetMovement += OnGetMovement;
    }

    private void OnDisable()
    {
        Player.Instance.OnGetMovement -= OnGetMovement;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        movement = new Vector3(direction.x, Rgbd.velocity.y, direction.y);

        Rgbd.velocity = movement * MoveSpeed;
        Rgbd.position = new Vector3
        (
            Mathf.Clamp(Rgbd.position.x, bounds.xMin, bounds.xMax),
            0.0f,
            Mathf.Clamp(Rgbd.position.z, bounds.zMin, bounds.zMax)
        );

        Rgbd.rotation = Quaternion.Euler(0, 0, Rgbd.velocity.x * -tiltValue);

        MoveScore();
    }

    private void MoveScore()
    {
        int moveScore = (int)(direction.x + direction.y);
        if (moveScore < 0) moveScore *= -1;
        GameController.Instance.ChangeScore(moveScore);
    }

    private void OnGetMovement(Vector2 dir)
    {
        direction = dir;
    }
}
