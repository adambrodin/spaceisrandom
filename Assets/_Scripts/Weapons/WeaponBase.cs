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
    protected bool playFireSound = true;
    [SerializeField]
    protected bool autoShoot, changeBulletToParentColor;
    [SerializeField]
    protected GameObject bulletObj;
    [SerializeField]
    protected GameObject[] firepoints;
    protected float WeaponCooldown => GetComponent<EntityBase>().stats.weaponCooldown;
    [SerializeField]
    protected AudioClip fireSound;
    [SerializeField]
    protected float fireSoundVolume = 0.25f; // How loud the fire/shoot sound will play

    // To fire the bullet(s) at the firepoints in order or all at all of them at once
    protected enum FireMode { cycling, multiple }
    [SerializeField]
    protected FireMode firingMode;
    protected int firepointToUse; // Firepoint to fire fram, default: 0 = first one in array
    #endregion

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
                SpawnBullet(bulletObj, firepoints[firepointToUse].transform.position, firepoints[firepointToUse].transform.rotation);

                firepointToUse++;
                if (firepointToUse >= firepoints.Length) { firepointToUse = 0; } // Prevent going out of array bounds
            }
            // Fire from all firepoints at once
            else { foreach (GameObject firepoint in firepoints) { SpawnBullet(bulletObj, firepoint.transform.position, firepoint.transform.rotation); } }

            // Wait before firing again
            if (WeaponCooldown > 0) StartCoroutine(Cooldown());
        }
        else { return; }
    }

    private void SpawnBullet(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject spawnedObj = Instantiate(obj, pos, rot);
        if (changeBulletToParentColor) { BulletToParentColor(spawnedObj); }
        if (playFireSound && fireSound != null) { SoundManager.Play(fireSound, fireSoundVolume); }
    }

    private void BulletToParentColor(GameObject obj)
    {
        Material m = new Material(obj.GetComponentInChildren<MeshRenderer>().sharedMaterial);
        if (GetComponent<EntityBase>().entityColors != null && GetComponent<EntityBase>().entityColors.Length > 0)
        {
            m.SetColor("_BaseColor", GetComponent<EntityBase>().entityColors[0]);
            m.SetColor("_EmissionColor", GetComponent<EntityBase>().entityColors[0]);
        }
        if (GetComponent<EntityBase>().entityChildColors != null && GetComponent<EntityBase>().entityChildColors.Length > 0)
        {
            m.SetColor("_BaseColor", GetComponent<EntityBase>().entityChildColors[0]);
            m.SetColor("_EmissionColor", GetComponent<EntityBase>().entityChildColors[0]);
        }
        obj.GetComponentInChildren<MeshRenderer>().material = m;
    }
}
