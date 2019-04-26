using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour
{
    public Entity stats;

    protected virtual void Start()
    {
        RandomizeColors();
    }

    protected void RandomizeColors()
    {
        foreach (Material m in GetComponentInChildren<MeshRenderer>().materials)
        {
            m.SetColor("_BaseColor", Random.ColorHSV());
        }
    }
}
