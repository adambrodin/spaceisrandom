/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System;

public class HealthPickup : PickupBase
{
    #region Variables
    public Action<int> OnChangePlayerHealth;
    #endregion

    protected override void PickupEffect() => OnChangePlayerHealth?.Invoke(1);
}
