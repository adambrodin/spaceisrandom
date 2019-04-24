using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player_Bullet_Default : MonoBehaviour
{
    #region Variables
    public Bullet bullet;
    private MeshRenderer meshRen;
    private Color parentColor;
    Rigidbody rgbd;
    #endregion

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        meshRen = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        parentColor = GameObject.Find("Player").GetComponentInChildren<MeshRenderer>().material.GetColor("_BaseColor");
        meshRen.material.SetColor("_BaseColor", parentColor);
    }

    // Move bullet
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
