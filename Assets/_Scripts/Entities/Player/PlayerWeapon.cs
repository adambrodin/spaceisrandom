using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PlayerWeapon : WeaponBase
{
    private int firepointToUse;
    private bool isFiring;

    public UnityEngine.Mesh bulletMesh;
    public UnityEngine.Material bulletMaterial;
    public bool useEcs;

    protected override void Start()
    {
        base.Start();
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
        if (useEcs)
        {
            EntityArchetype bulletArcheType = entityManager.CreateArchetype
                (
                typeof(BulletComponent),
                typeof(Translation),
                typeof(RenderMesh),
                typeof(LocalToWorld),
                typeof(PhysicsVelocity)
                );

            NativeArray<Entity> bulletArray = new NativeArray<Entity>(50, Allocator.Temp);

            entityManager.CreateEntity(bulletArcheType, bulletArray);

            for (int i = 0; i < bulletArray.Length; i++)
            {
                Entity e = bulletArray[i];

                entityManager.SetComponentData(e, new BulletComponent { moveSpeed = UnityEngine.Random.Range(50, 100) });
                entityManager.SetComponentData(e, new Translation { Value = new float3(firepoints[firepointToUse].transform.position) });
                entityManager.SetSharedComponentData(e, new RenderMesh { mesh = bulletMesh, material = bulletMaterial });
                entityManager.SetComponentData(e, new PhysicsVelocity { Linear = 1000 });
            }

            bulletArray.Dispose();
        }
        else
        {
            GameObject g = Instantiate(bulletObj, firepoints[firepointToUse].transform.position, bulletObj.transform.rotation);
            Color parentColor = GetComponentInChildren<MeshRenderer>().materials[0].color;
            g.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", parentColor);

            firepointToUse++;
            if (firepointToUse >= firepoints.Length)
            {
                firepointToUse = 0;
            }

            StartCoroutine(Cooldown());
        }
    }

    private void Update()
    {
        CheckForFire();
    }
}
