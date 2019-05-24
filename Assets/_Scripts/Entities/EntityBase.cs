using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
public class RendererInfo
{
    public Renderer renderer;
    public Material[] materials;

    public RendererInfo(Renderer rend, Material[] mats)
    {
        this.renderer = rend;
        this.materials = mats;
    }
}
public abstract class EntityBase : MonoBehaviour
{
    public EntityStats stats;
    [HideInInspector]
    public Renderer[] renderers;
    public RendererInfo[] renderersInfo;

    protected virtual void Awake() => RandomizeColors();

    /// <summary>
    /// Finds the correct MeshRenderer and sets a randomized color
    /// based on a set color scheme (RandomColorRange) for all materials in the renderer
    /// </summary>
    protected void RandomizeColors()
    {
        renderers = new Renderer[FindObjectsOfType<Renderer>().Length];
        renderersInfo = new RendererInfo[renderers.Length];

        for (int rend = 0; rend < renderers.Length; rend++)
        {
            renderers[rend] = FindObjectsOfType<Renderer>()[rend];
            for (int mat = 0; mat < renderers[rend].materials.Length; mat++)
            {
                renderersInfo[rend] = new RendererInfo(renderers[rend], renderers[rend].materials);
                renderersInfo[rend].materials[mat].SetColor("_BaseColor", RandomizedColor());
                renderersInfo[rend].materials[mat].SetColor("_EmissionColor", RandomizedColor());
            }
        }
    }

    private Color RandomizedColor()
    {
        RandomColorRange r = GameController.Instance.randomColorRange;
        return Random.ColorHSV(r.hueMin, r.hueMax, r.saturationMin, r.saturationMax, r.valueMin, r.valueMax, r.alphaMin, r.alphaMax);
    }
}
