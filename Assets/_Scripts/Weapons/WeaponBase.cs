/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System.Collections;
using UnityEngine;
public abstract class WeaponBase : MonoBehaviour
{
    #region Variables
    protected bool canShoot = true;
    [SerializeField]
    protected bool autoShoot, changeBulletToParentColor, playFireSound = true;
    [SerializeField]
    protected GameObject bulletObj;
    [SerializeField]
    protected GameObject[] firepoints;
    protected float WeaponCooldown => GetComponent<EntityBase>().stats.weaponCooldown;
    [SerializeField]
    protected string fireSoundName;

    // To fire the bullet(s) at the firepoints in order or all at all of them at once
    protected enum FireMode { cycling, multiple }
    [SerializeField]
    protected FireMode firingMode;
    protected int firepointToUse; // Firepoint to fire fram, default: 0 = first one in array
    #endregion

    protected IEnumerator Cooldown()
    {
        if (canShoot) { canShoot = false; }
        else { yield break; } // Exit the couroutine to prevent unnecessary compiling

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
    }

    private void SpawnBullet(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject spawnedObj = Instantiate(obj, pos, rot);
        if (changeBulletToParentColor) { BulletToParentColor(spawnedObj); }
        if (playFireSound && fireSoundName != null && fireSoundName != "")
        {
            float randomPitch = UnityEngine.Random.Range(0.9f, 1f);
            FindObjectOfType<AudioManager>().Set(fireSoundName, randomPitch, FindObjectOfType<AudioManager>().GetSound(fireSoundName).source.volume);
            FindObjectOfType<AudioManager>().SetPlaying(fireSoundName, true);
        }
    }

    private void BulletToParentColor(GameObject obj)
    {
        Material m = new Material(obj.GetComponentInChildren<MeshRenderer>().sharedMaterial);
        GetComponent<EntityBase>().originalMaterials.TryGetValue(0, out Material[] parentMaterials);
        m.SetColor("_BaseColor", parentMaterials[0].GetColor("_BaseColor"));
        m.SetColor("_EmissionColor", parentMaterials[0].GetColor("_EmissionColor"));
        obj.GetComponentInChildren<MeshRenderer>().material = m;
    }
}
