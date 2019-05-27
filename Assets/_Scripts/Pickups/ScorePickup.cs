/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using UnityEngine;

public class ScorePickup : PickupBase
{
    #region Variables
    [SerializeField]
    private float minReward, maxReward;
    #endregion
    protected override void PickupEffect() => GameController.Instance.AddScore((int)Mathf.Ceil(UnityEngine.Random.Range(minReward, maxReward)));
}
