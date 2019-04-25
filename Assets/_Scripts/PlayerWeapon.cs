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
        GameObject g = Instantiate(bulletObj, gameObject.transform);
        Color parentColor = gameObject.GetComponentInChildren<MeshRenderer>().materials[0].color;
        g.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", parentColor);
    }

    private void Update()
    {
        CheckForFire();
    }
}
