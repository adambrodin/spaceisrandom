using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public float
        projectileSpeed,
        projectileDamage,
        projectileCooldown;
}
