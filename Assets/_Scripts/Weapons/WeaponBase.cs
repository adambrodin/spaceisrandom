using System.Collections;
using System.Linq;
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
                GameObject clone = Instantiate(bulletObj, firepoints[firepointToUse].transform.position, firepoints[firepointToUse].transform.rotation);

                if (changeBulletToParentColor)
                {
                    Material m = new Material(clone.GetComponentInChildren<MeshRenderer>().sharedMaterial);
                    m.SetColor("_Basecolor", GetComponent<EntityBase>().entityColors[0]);
                    m.SetColor("_EmissionColor", GetComponent<EntityBase>().entityColors[0]);
                    clone.GetComponentInChildren<MeshRenderer>().material = m;
                }

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
