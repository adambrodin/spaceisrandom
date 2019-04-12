﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IMoveable
{
    float moveSpeed { get; set; }

    Rigidbody rgbd { get; }

    void Move();
}
