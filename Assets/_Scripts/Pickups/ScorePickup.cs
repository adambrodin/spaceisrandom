/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using UnityEngine;

public class ScorePickup : PickupBase
{
    #region Variables
    [SerializeField]
    private float minReward, maxReward;
    #endregion
    protected override void PickupEffect() => GameController.Instance.AddScore((int)Mathf.Ceil(UnityEngine.Random.Range(minReward, maxReward)));
}
