using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class EnemyMovement : TargetTracker
{
    #region Variables
    private void Start()
    {
        moveSpeed = GetComponent<Enemy>().getStats().moveSpeed;
    }
    #endregion
}
