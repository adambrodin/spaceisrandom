using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerWeapon : WeaponBase
{
    private int firepointToUse = 0; // Which firepoint to shoot from

    private void Start()
    {
        firepointToUse = 0;
        StartCoroutine(Cooldown());
    }

    protected override void CheckForFire()
    {
        if (Input.GetAxis("Fire1") > 0 || Input.GetButton("B") || Input.GetKey(KeyCode.Space))
        {
            if (canFire)
            {
                Fire();

                StartCoroutine(Cooldown());
            }
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

    }

    private void Update()
    {
        CheckForFire();
    }
}
