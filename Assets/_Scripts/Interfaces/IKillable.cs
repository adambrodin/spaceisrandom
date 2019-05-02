/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IKillable<T>
{
    float StartHealth { get; set; }
    float CurrentHealth { get; set; }

    void TakeDamage(float T);
    bool IsDead();
    void Die();
}
