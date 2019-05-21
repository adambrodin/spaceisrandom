using UnityEngine;
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class TurretWeapon : WeaponBase
{
    #region Variables
    [SerializeField]
    private float allFireChance; // Chance of the weapon shooting with all firepoints
    private static System.Random randomizer;
    #endregion

    private void Start()
    {
        if (ChanceChecker(allFireChance))
        {
            firingMode = FireMode.multiple;
        }
        else { firingMode = FireMode.cycling; }
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
