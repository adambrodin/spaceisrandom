using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "EnemyObject")]
public class EnemyObject : ScriptableObject
{
    public int damage, health;
    public float moveSpeed, projectileSpeed, shootCooldown;
}