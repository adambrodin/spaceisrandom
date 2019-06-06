/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System;

public class HealthPickup : PickupBase
{
    #region Variables
    public event Action<int> OnChangePlayerHealth;
    #endregion

    private void Awake() => OnChangePlayerHealth += FindObjectOfType<Player>().ChangeHealth;
    protected override void PickupEffect() => OnChangePlayerHealth?.Invoke(1);
}
