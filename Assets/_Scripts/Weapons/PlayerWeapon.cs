/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System.Collections;
using UnityEngine;
public class PlayerWeapon : WeaponBase
{
    private bool isFiring;

    private void OnEnable() => Player.Instance.OnGetShooting += OnGetShooting;
    private void OnDisable() => Player.Instance.OnGetShooting -= OnGetShooting;
    private void OnGetShooting(bool value) => isFiring = value;

    private void CheckForFire() { if (isFiring) { Shoot(); } }
    private void Update() => CheckForFire();
    public void OneShotKill(float duration) => StartCoroutine(OneShotKillTimer(duration));

    private IEnumerator OneShotKillTimer(float duration)
    {
        bulletObj.GetComponent<BulletBase>().oneShotKill = true;
        yield return new WaitForSeconds(duration);
        bulletObj.GetComponent<BulletBase>().oneShotKill = false;
    }
}
