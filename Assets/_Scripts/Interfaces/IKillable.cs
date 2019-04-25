using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IKillable<T>
{
    float StartHealth { get; set; }
    float CurrentHealth { get; set; }

    void TakeDamage(T damage);
    bool IsDead();
    void Die();
}
