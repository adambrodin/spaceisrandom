using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerWeapon : WeaponBase
{
    private int firepointToUse = 0; // Which firepoint to shoot from
    private bool isFiring;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Cooldown());
    }

    private void OnEnable()
    {
        Player.Instance.OnGetShooting += OnGetShooting;
    }

    private void OnDisable()
    {
        Player.Instance.OnGetShooting -= OnGetShooting;
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
        //GameObject g = Instantiate(bulletObj, firepoints[firepointToUse].transform.position, bulletObj.transform.rotation);

        for (int i = 0; i <= 100; i++)
        {
            var e = entityManager.Instantiate(bulletEntity);
            print("Instantiated Entity");
        }
        //float3 position = transform.TransformPoint(firepoints[firepointToUse].transform.position);
        //entityManager.SetComponentData(e, new Translation { Value = position });
        //print("Entity position changed");

        firepointToUse++;
        if (firepointToUse >= firepoints.Length)
        {
            firepointToUse = 0;
        }

        //Color parentColor = GetComponentInChildren<MeshRenderer>().materials[0].color;
        //g.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", parentColor);

        StartCoroutine(Cooldown());
    }

    private void Update()
    {
        CheckForFire();
    }
}
