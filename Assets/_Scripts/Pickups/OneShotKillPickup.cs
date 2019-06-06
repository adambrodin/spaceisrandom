/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using System;
using UnityEngine;

public class OneShotKillPickup : PickupBase
{
    #region Variables
    [SerializeField]
    private float minDuration, maxDuration;
    public event Action<float> OnOneShotKill;
    #endregion

    private void Awake() => OnOneShotKill += FindObjectOfType<PlayerWeapon>().OneShotKill;
    protected override void PickupEffect() => OnOneShotKill?.Invoke(UnityEngine.Random.Range(minDuration, maxDuration));
}
