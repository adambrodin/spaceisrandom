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
            // Exit the couroutine to save resources
            yield break;
        }

        yield return new WaitForSeconds(GetComponent<EntityBase>().getStats().weaponCooldown);

        canFire = true;
    }

    protected virtual void Start()
    {
        entityManager = World.Active.EntityManager;
    }
}
