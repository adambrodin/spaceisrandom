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
    public Action<float> OnOneShotKill;
    #endregion

    protected override void PickupEffect() => OnOneShotKill?.Invoke(UnityEngine.Random.Range(minDuration, maxDuration));
}
