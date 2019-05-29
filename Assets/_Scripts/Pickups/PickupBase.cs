/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour, IPickupable
{
    #region Variables
    public Action OnPickup;
    public string PickupSoundName => pickupSoundName;
    [SerializeField]
    protected string pickupSoundName;
    #endregion

    public void PickUp()
    {
        PickupEffect();
        AudioManager.Instance.SetPlaying(pickupSoundName, true);
        OnPickup?.Invoke();
        Destroy(gameObject);
    }

    protected abstract void PickupEffect();
}
