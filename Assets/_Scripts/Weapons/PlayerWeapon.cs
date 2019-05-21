using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
public class PlayerWeapon : WeaponBase
{
    private bool isFiring;

    private void OnEnable() => Player.Instance.OnGetShooting += OnGetShooting;
    private void OnDisable() => Player.Instance.OnGetShooting -= OnGetShooting;
    private void OnGetShooting(bool value) => isFiring = value;

    private  void CheckForFire()
    {
        if (isFiring) { Shoot(); }
    }

    private void Update() => CheckForFire();
}
