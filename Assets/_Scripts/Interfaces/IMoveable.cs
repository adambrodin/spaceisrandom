using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IMoveable
{
    #region Variables
    float moveSpeed { get; set; }
    #endregion

    void Move();
}
