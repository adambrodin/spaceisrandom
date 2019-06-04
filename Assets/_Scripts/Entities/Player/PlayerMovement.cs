/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region Variables
    [SerializeField]
    private float tiltSpeed, minTilt, maxTilt;
    public float MoveSpeed { get; set; }
    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    private Vector3 movement, direction;
    #endregion

    private void Start() => MoveSpeed = GetComponent<EntityBase>().stats.moveSpeed;
    private void OnEnable() => Player.Instance.OnGetMovement += OnGetMovement;
    private void OnDisable() => Player.Instance.OnGetMovement -= OnGetMovement;

    // Get the input vector2 values
    private void OnGetMovement(Vector2 dir) => direction = dir;
    private void FixedUpdate() => Move();

    public void Move()
    {
        // Set movement boundries for the level
        Rgbd.position = new Vector3
       (
           Mathf.Clamp(Rgbd.position.x, GameController.Instance.bounds.xMin, GameController.Instance.bounds.xMax),
           Rgbd.position.y,
           Mathf.Clamp(Rgbd.position.z, GameController.Instance.bounds.zMin, GameController.Instance.bounds.zMax)
       );

        // Move the player
        movement = new Vector3(direction.x, Rgbd.velocity.y, direction.y);
        Rgbd.velocity = movement * MoveSpeed;
        Rgbd.rotation = Quaternion.Euler(Rgbd.rotation.x, Rgbd.rotation.y, Mathf.Clamp(Rgbd.velocity.x * -tiltSpeed, minTilt, maxTilt));
        // Add score whenever the player moves inside the level
        MoveScore();
    }

    private void MoveScore()
    {
        // If the player is within the level bounds
        if
        (
            Rgbd.position.x != GameController.Instance.bounds.xMin && Rgbd.position.x != GameController.Instance.bounds.xMax
            &&
            Rgbd.position.z != GameController.Instance.bounds.zMin && Rgbd.position.z != GameController.Instance.bounds.zMax
        )
        {
            // Get the input values as absolute numbers (only positive)
            int moveScore = (int)(Mathf.Abs(direction.x) + Mathf.Abs(direction.y));

            // Avoid constantly changing score by 0 when not moving
            if (moveScore != 0) GameController.Instance.AddScore(moveScore);
        }
        else { return; } // Out of bounds
    }
}
