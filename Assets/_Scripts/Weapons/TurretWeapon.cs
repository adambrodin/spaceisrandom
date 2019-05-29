/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using UnityEngine;

public class TurretWeapon : WeaponBase
{
    #region Variables
    [SerializeField]
    private float allFireChance; // Chance of the weapon shooting with all firepoints
    private static System.Random randomizer;
    #endregion

    private void Start()
    {
        if (ChanceChecker(allFireChance)) { firingMode = FireMode.multiple; }
        else { firingMode = FireMode.cycling; }

        if (bulletObj.GetComponent<TrailRenderer>() != null && GetComponent<EntityBase>() != null)
        {
            if (bulletObj.GetComponent<TrailRenderer>().sharedMaterial != null)
            {
                GetComponent<EntityBase>().originalMaterials.TryGetValue(0, out Material[] parentMaterials);
                bulletObj.GetComponent<TrailRenderer>().sharedMaterial = new Material(bulletObj.GetComponent<TrailRenderer>().sharedMaterial);
                bulletObj.GetComponent<TrailRenderer>().sharedMaterial.SetColor("_Color", parentMaterials[0].GetColor("_Color"));
                bulletObj.GetComponent<TrailRenderer>().sharedMaterial.SetColor("_BaseColor", parentMaterials[0].GetColor("_BaseColor"));
                bulletObj.GetComponent<TrailRenderer>().sharedMaterial.SetColor("_EmissionColor", parentMaterials[0].GetColor("_EmissionColor"));
                // bulletObj.GetComponent<TrailRenderer>().material.SetColor("_BaseColor",
                // parentMaterials[0].GetColor("_BaseColor"));

                // bulletObj.GetComponent<TrailRenderer>().material.SetColor("_EmissionColor",
                // parentMaterials[0].GetColor("_EmissionColor"));
            }
        }
    }

    private bool ChanceChecker(float chance)
    {
        if (randomizer == null) { randomizer = new System.Random(); }
        if (randomizer.Next(100) < chance)
        {
            return true;
        }
        return false;
    }
}
