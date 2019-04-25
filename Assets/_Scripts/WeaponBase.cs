using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected GameObject bulletObj;
    public Entity stats;

    protected bool canFire = true;

    protected abstract void CheckForFire();
    protected abstract void Fire();

    protected IEnumerator Cooldown()
    {
        if (canFire == true)
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
