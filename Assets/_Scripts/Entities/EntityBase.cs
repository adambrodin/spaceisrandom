using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour, IMoveable
{
    [SerializeField]
    protected Entity stats;

    public Rigidbody Rgbd => GetComponent<Rigidbody>();

    public float MoveSpeed { get => stats.moveSpeed; set => stats.moveSpeed = value; }

    public abstract void Move();

    protected void RandomizeColors()
    {
        foreach (Material m in GetComponentInChildren<MeshRenderer>().materials)
        {
            m.SetColor("_BaseColor", UnityEngine.Random.ColorHSV());
        }
    }
}
