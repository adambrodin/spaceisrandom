using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletObj;
    [SerializeField]
    protected Entity stats;
    protected bool canFire = true;
    [SerializeField]

    protected GameObject[] firepoints;

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

        yield return new WaitForSeconds(stats.weaponCooldown);

        canFire = true;
    }
}
