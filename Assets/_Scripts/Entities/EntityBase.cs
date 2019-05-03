using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour
{
    public EntityStats stats;
    [HideInInspector]
    public Color[] entityColors;

    protected virtual void Start()
    {
        RandomizeColors();
    }

    public EntityStats getStats()
    {
        return stats;
    }

    protected void RandomizeColors()
    {
        MeshRenderer meshRen;
        if (GetComponent<MeshRenderer>() != null)
        {
            meshRen = GetComponent<MeshRenderer>();
        }
        else
        {
            meshRen = GetComponentInChildren<MeshRenderer>();
        }

        entityColors = new Color[meshRen.materials.Length];
        RandomColorRange r = GameController.Instance.randomColorRange;

        for (int i = 0; i < entityColors.Length; i++)
        {
            entityColors[i] = Random.ColorHSV(r.hueMin, r.hueMax, r.saturationMin, r.saturationMax, r.valueMin, r.valueMax, r.alphaMin, r.alphaMax);
            meshRen.materials[i].SetColor("_BaseColor", entityColors[i]);
        }
    }
}
