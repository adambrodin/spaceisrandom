using System.Collections;
using UnityEngine;
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
    private Bounds bounds;
    private Vector3 movement, direction;
    #endregion

    private void Start()
    {
        MoveSpeed = Player.Instance.GetComponent<EntityBase>().stats.moveSpeed;
        bounds = GameController.Instance.bounds;
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
        // Set movement boundries for the level
        Rgbd.position = new Vector3
        (
            Mathf.Clamp(Rgbd.position.x, bounds.xMin, bounds.xMax),
            Rgbd.position.y,
            Mathf.Clamp(Rgbd.position.z, bounds.zMin, bounds.zMax)
        );

        // Move the player
        movement = new Vector3(direction.x, Rgbd.velocity.y, direction.y);
        Rgbd.velocity = movement * MoveSpeed;
        Rgbd.rotation = Quaternion.Euler(0, 0, Rgbd.velocity.x * -tiltValue);

        // Add score whenever the player moves inside the level
        MoveScore();
    }

    private void MoveScore()
    {
        // If the player is within the level bounds
        if
        (
            Rgbd.position.x != bounds.xMin && Rgbd.position.x != bounds.xMax
            &&
            Rgbd.position.z != bounds.zMin && Rgbd.position.z != bounds.zMax
        )
        {
            // Get the input values as absolute numbers (only positive)
            int moveScore = (int)(Mathf.Abs(direction.x) + Mathf.Abs(direction.y));

            // Avoid constantly changing score by 0 when not moving
            if (moveScore != 0) GameController.Instance.ChangeScore(moveScore);
        }
        else
        {
            // Outside of level bounds
            return;
        }
    }

    private void OnGetMovement(Vector2 dir)
    {
        // Get the input vector2 values
        direction = dir;
    }
}
