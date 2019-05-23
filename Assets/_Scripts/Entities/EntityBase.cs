using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
public abstract class EntityBase : MonoBehaviour
{
    public EntityStats stats;
    [HideInInspector]
    public Color[] entityColors, entityChildColors;
    protected MeshRenderer meshRen, childMeshRen;

    protected virtual void Awake()
    {
        if (GetComponent<MeshRenderer>() != null) meshRen = GetComponent<MeshRenderer>();
        if (GetComponentInChildren<MeshRenderer>() != null) childMeshRen = GetComponentInChildren<MeshRenderer>();

        RandomizeColors();
    }

    /// <summary>
    /// Finds the correct MeshRenderer and sets a randomized color
    /// based on a set color scheme (RandomColorRange) for all materials in the renderer
    /// </summary>
    protected void RandomizeColors()
    {
        RandomColorRange r = GameController.Instance.randomColorRange;
        if (meshRen != null)
        {
            entityColors = new Color[meshRen.materials.Length];
            for (int i = 0; i < entityColors.Length; i++)
            {
                entityColors[i] = Random.ColorHSV(r.hueMin, r.hueMax, r.saturationMin, r.saturationMax, r.valueMin, r.valueMax, r.alphaMin, r.alphaMax);
                meshRen.materials[i].SetColor("_BaseColor", entityColors[i]);
                meshRen.materials[i].SetColor("_EmissionColor", entityColors[i]);
            }
        }

        if (childMeshRen != null)
        {
            entityChildColors = new Color[childMeshRen.materials.Length];
            for (int i = 0; i < entityChildColors.Length; i++)
            {
                entityChildColors[i] = Random.ColorHSV(r.hueMin, r.hueMax, r.saturationMin, r.saturationMax, r.valueMin, r.valueMax, r.alphaMin, r.alphaMax);
                childMeshRen.materials[i].SetColor("_BaseColor", entityChildColors[i]);
                childMeshRen.materials[i].SetColor("_EmissionColor", entityChildColors[i]);
            }
        }
    }
}
