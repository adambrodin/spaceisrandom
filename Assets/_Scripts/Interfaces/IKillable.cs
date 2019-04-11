using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IKillable<T>
{
    #region Variables
    float startHealth { get; set; }
    float currentHealth { get; set; }

    void TakeDamage(T damage);
    void Die();
    #endregion
}
