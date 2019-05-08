using System.Collections;
using Unity.Entities;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public abstract class WeaponBase : MonoBehaviour
{
    #region Variables
    protected EntityManager entityManager;
    protected bool canFire = true;
    [SerializeField]
    protected GameObject bulletObj;
    [SerializeField]
    protected GameObject[] firepoints;

    protected float WeaponCooldown => GetComponent<EntityBase>().stats.weaponCooldown;
    #endregion

    protected abstract void CheckForFire();
    protected abstract void Fire();
    protected IEnumerator Cooldown()
    {
        if (canFire)
        {
            canFire = false;
        }
        else
        {
            // Exit the couroutine to prevent unnecessary compiling
            yield break;
        }

        yield return new WaitForSeconds(WeaponCooldown);
        canFire = true;
    }

    protected virtual void Start()
    {
        // Get the EntityManager of the world
        entityManager = World.Active.EntityManager;
    }
}
