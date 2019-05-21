using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
public class PlayerWeapon : WeaponBase
{
    private int firepointToUse;
    private bool isFiring;

    private void OnEnable() => Player.Instance.OnGetShooting += OnGetShooting;

    private void OnDisable() => Player.Instance.OnGetShooting -= OnGetShooting;

    private void OnGetShooting(bool value) => isFiring = value;

    protected override void CheckForFire()
    {
        if (canFire && isFiring)
        {
            Fire();
        }
    }

    protected override void Fire()
    {
        GameObject g = Instantiate(bulletObj, firepoints[firepointToUse].transform.position, bulletObj.transform.rotation);
        Color parentColor = GetComponentInChildren<MeshRenderer>().materials[0].color;
        g.GetComponentInChildren<MeshRenderer>().material.SetColor("_BaseColor", parentColor);

        firepointToUse++;
        if (firepointToUse >= firepoints.Length) { firepointToUse = 0; }

        StartCoroutine(Cooldown());
    }

    private void Update() => CheckForFire();
}
