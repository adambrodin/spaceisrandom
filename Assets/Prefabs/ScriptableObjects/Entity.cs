using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public float
        moveSpeed,
        startHealth,
        projectileSpeed,
        projectileDamage,
        projectileCooldown;
}
