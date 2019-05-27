/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System;
using UnityEngine;

public class OneShotKillPickup : PickupBase
{
    #region Variables
    [SerializeField]
    private float minDuration, maxDuration;
    public static Action<float> OnOneShotKill;
    #endregion

    protected override void PickupEffect() => OnOneShotKill?.Invoke(UnityEngine.Random.Range(minDuration, maxDuration));
}
