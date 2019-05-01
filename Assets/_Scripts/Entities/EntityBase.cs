using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityBase : MonoBehaviour
{
    public Entity stats;
    [HideInInspector]
    public Color[] entityColors;

    protected virtual void Start()
    {
        RandomizeColors();
    }

    public Entity getStats()
    {
        return stats;
    }

    protected void RandomizeColors()
    {
        entityColors = new Color[GetComponentInChildren<MeshRenderer>().materials.Length];
        RandomColorRange r = GameController.instance.randomColorRange;

        for (int i = 0; i < entityColors.Length; i++)
        {
            entityColors[i] = Random.ColorHSV(r.hueMin, r.hueMax, r.saturationMin, r.saturationMax, r.valueMin, r.valueMax, r.alphaMin, r.alphaMax);
            GetComponentInChildren<MeshRenderer>().materials[i].SetColor("_BaseColor", entityColors[i]);
        }
    }
}
