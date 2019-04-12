using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IKillable
{
    float startHealth { get; set; }
    float currentHealth { get; set; }

    void TakeDamage(float damage);
    void Die();
}
