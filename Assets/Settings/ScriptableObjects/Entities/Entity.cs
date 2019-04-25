using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public float
        moveSpeed,
        startHealth,
        weaponCooldown,
        killReward;

    public enum EntityType
    {
        Player,
        Enemy,
    }

    public EntityType type; // Referring to the enum
}
