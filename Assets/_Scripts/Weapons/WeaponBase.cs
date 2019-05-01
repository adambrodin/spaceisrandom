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
    protected static EntityManager entityManager;
    protected Entity bulletEntity;
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
            yield break; // Exit the couroutine to save resources
        }

        yield return new WaitForSeconds(GetComponent<EntityBase>().getStats().weaponCooldown);

        canFire = true;
    }

    protected virtual void Start()
    {
        bulletEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletObj, World.Active);
        entityManager = World.Active.EntityManager;
        print("Converted prefab to Entity");
    }
}
