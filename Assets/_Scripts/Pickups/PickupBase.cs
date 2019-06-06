/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour, IPickupable
{
    #region Variables
    public event Action OnPickup;
    public string[] PickupSounds => pickupSounds;
    [SerializeField]
    protected string[] pickupSounds;
    #endregion

    private void Start() => OnPickup += FindObjectOfType<PickupSpawner>().PickedUp;
    public void PickUp()
    {
        if (pickupSounds.Length > 0) { AudioManager.Instance.SetPlaying(PickupSounds[UnityEngine.Random.Range(0, PickupSounds.Length)], true); }
        OnPickup?.Invoke();
        PickupEffect();
        Destroy(gameObject);
    }

    protected abstract void PickupEffect();
}
