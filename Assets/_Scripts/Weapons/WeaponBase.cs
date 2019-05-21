using System.Collections;
using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
public abstract class WeaponBase : MonoBehaviour
{
    #region Variables
    protected bool canShoot = true;
    [SerializeField]
    protected bool autoShoot, changeBulletToParentColor;
    [SerializeField]
    protected GameObject bulletObj;
    [SerializeField]
    protected GameObject[] firepoints;
    protected float WeaponCooldown => GetComponent<EntityBase>().stats.weaponCooldown;

    // To fire the bullet(s) at the firepoints in order or all at all of them at once
    protected enum FireMode
    {
        cycling,
        multiple
    }

    [SerializeField]
    protected FireMode firingMode;
    protected int firepointToUse; // Firepoint to fire fram, default: 0 = first one in array
    #endregion

    private void Start()
    {
        if (changeBulletToParentColor)
        {
            // TODO change bullet color
        }
    }

    protected IEnumerator Cooldown()
    {
        if (canShoot) { canShoot = false; }
        // Exit the couroutine to prevent unnecessary compiling
        else { yield break; }

        yield return new WaitForSeconds(WeaponCooldown);
        canShoot = true;

        if (autoShoot) { Shoot(); }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            // Shoot from each firepoint in order
            if (firingMode == FireMode.cycling)
            {
                Instantiate(bulletObj, firepoints[firepointToUse].transform.position, firepoints[firepointToUse].transform.rotation);

                firepointToUse++;
                if (firepointToUse >= firepoints.Length) { firepointToUse = 0; } // Prevent going out of array bounds
            }
            // Fire from all firepoints at once
            else
            {
                foreach (GameObject firepoint in firepoints)
                {
                    Instantiate(bulletObj, firepoint.transform.position, firepoint.transform.rotation);
                }
            }

            // Wait before firing again
            if (WeaponCooldown > 0) StartCoroutine(Cooldown());
        }
        else { return; }
    }
}
