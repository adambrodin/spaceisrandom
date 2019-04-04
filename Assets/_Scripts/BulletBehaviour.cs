using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    public Bullet bullet;
    Rigidbody rgbd;
    #endregion

    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rgbd.velocity = new Vector3(0, 0, bullet.projectileSpeed);
    }
}
