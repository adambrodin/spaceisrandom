using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerWeapon : WeaponBase
{
    private int firepointToUse = 0; // Which firepoint to shoot from
    private bool isFiring;
    private void Start()
    {
        StartCoroutine(Cooldown());
    }

    private void OnEnable()
    {
        Player.OnGetShooting += OnGetShooting;
    }

    private void OnDisable()
    {
        Player.OnGetShooting -= OnGetShooting;
    }

    private void OnGetShooting(bool value)
    {
        isFiring = value;
    }

    protected override void CheckForFire()
    {
        if (canFire && isFiring)
        {
            Fire();
        }
    }

    protected override void Fire()
    {
        GameObject g = Instantiate(bulletObj, firepoints[firepointToUse].transform.position, firepoints[firepointToUse].transform.rotation);

        firepointToUse++;
        if (firepointToUse >= firepoints.Length)
        {
            firepointToUse = 0;
        }

        Color parentColor = gameObject.GetComponentInChildren<MeshRenderer>().materials[0].color;
        g.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", parentColor);

        StartCoroutine(Cooldown());
    }

    private void Update()
    {
        CheckForFire();
    }
}
