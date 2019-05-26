using System.Collections.Generic;
using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public abstract class EntityBase : MonoBehaviour
{
    #region Variables
    public EntityStats stats;
    [HideInInspector]
    public Renderer[] renderers;

    /// <param name="int">Renderer Index</param>
    public Dictionary<int, Material[]> originalMaterials = new Dictionary<int, Material[]>();
    #endregion

    protected virtual void Awake()
    {
        RandomizeColors();
    }

    /// <summary>
    /// Loops through each Renderer in the object and sets the color(s) of the material(s) based on a set color scheme (RandomColorRange) 
    /// </summary>
    protected void RandomizeColors()
    {
        renderers = new Renderer[GetComponentsInChildren<Renderer>().Length];
        for (int rend = 0; rend < renderers.Length; rend++)
        {
            renderers[rend] = GetComponentsInChildren<Renderer>()[rend];
            for (int mat = 0; mat < renderers[rend].materials.Length; mat++)
            {
                Color randomizedColor = RandomizedColor();
                renderers[rend].materials[mat].SetColor("_BaseColor", randomizedColor);
                renderers[rend].materials[mat].SetColor("_EmissionColor", randomizedColor);
            }

            Material[] originalMatArray = new Material[renderers[rend].materials.Length];
            for (int i = 0; i < originalMatArray.Length; i++) { originalMatArray[i] = new Material(renderers[rend].materials[i]); }
            originalMaterials.Add(rend, originalMatArray);
        }
    }

    private Color RandomizedColor()
    {
        RandomColorRange r = GameController.Instance.randomColorRange;
        return Random.ColorHSV(r.hueMin, r.hueMax, r.saturationMin, r.saturationMax, r.valueMin, r.valueMax, r.alphaMin, r.alphaMax);
    }
}
