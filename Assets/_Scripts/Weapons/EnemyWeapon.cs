/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System.Collections;
using UnityEngine;

public class EnemyWeapon : WeaponBase
{
    private void Start()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        yield return new WaitUntil(() => GetComponent<Rigidbody>().position.z <= GameController.Instance.bounds.zMax);
        Shoot();
    }
}
