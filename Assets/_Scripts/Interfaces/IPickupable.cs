using System;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IPickupable<T>
{
    event Action<T> OnPickUp;
    void PlayPickUpEffect();
}
