using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerMovement : MonoBehaviour, IMoveable
{
    #region Variables
    public float tiltValue;
    public float MoveSpeed { get; set; }

    public Rigidbody Rgbd => GetComponent<Rigidbody>();
    private Vector2 direction;
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
        Vector3 movement = new Vector3(direction.x, Rgbd.velocity.y, direction.y);

        Rgbd.velocity = movement * MoveSpeed;
        Rgbd.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rgbd.velocity.x * -tiltValue);
    }

    private void OnGetMovement(Vector2 dir)
    {
        direction = dir;
    }
}
