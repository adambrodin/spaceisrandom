using System.Collections;
using UnityEngine;
#pragma warning disable 649 // Disable incorrect warnings in the console (for serializefield private objects)
#pragma warning disable IDE0044 // ^ but for visual studio
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
public class TurretWeapon : TargetTracker
{
    #region Variables
    [SerializeField]
    private float damping, cooldown;
    private int firepointToUse;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject[] firepoints;
    #endregion

    private void Start() => StartCoroutine(Shoot());

    protected override void CalculateMovement()
    {
        targetDir = targetRgbd.position - rgbd.position;
        newDir = Vector3.Lerp(Vector3.back, targetDir, damping * Time.deltaTime);
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(cooldown);
        Instantiate(bullet, firepoints[firepointToUse].transform.position, rgbd.rotation);

        firepointToUse++;
        if (firepointToUse >= firepoints.Length) { firepointToUse = 0; }
        StartCoroutine(Shoot());
    }
}
