using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class BulletSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref BulletComponent bulletComponent, ref PhysicsVelocity velocity) =>
        {
            translation.Value.z += bulletComponent.moveSpeed * Time.deltaTime;

            //velocity.Linear += 1000;
            //velocity.Angular += 1000;
        });
    }
}
