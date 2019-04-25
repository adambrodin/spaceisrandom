using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerWeapon : WeaponBase
{
    private void Start()
    {
        StartCoroutine(Cooldown());
    }

    protected override void CheckForFire()
    {
        if (Input.GetAxis("Fire2") > 0 || Input.GetAxis("B") > 0)
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
        GameObject g = Instantiate(bulletObj);
        gameObject.GetComponent<MeshRenderer>().material.color = g.GetComponent<MeshRenderer>().materials[0].color;
    }

    private void Update()
    {
        CheckForFire();
    }
}
