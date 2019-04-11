using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    public Bullet bullet;
    private MeshRenderer meshRen;
    Rigidbody rgbd;
    #endregion

    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        meshRen = GetComponent<MeshRenderer>();

        meshRen.material.SetColor("_Color", Random.ColorHSV());
    }

    private void Update()
    {
        rgbd.velocity = new Vector3(0, 0, bullet.projectileSpeed);
    }

    // Destroy self when out of rendered area
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
