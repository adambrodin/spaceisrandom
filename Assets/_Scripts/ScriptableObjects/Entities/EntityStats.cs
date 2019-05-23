using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Entity")]
public class EntityStats : ScriptableObject
{
    public float moveSpeed, startHealth, weaponCooldown, killReward;
    public enum EntityType
    {
        Player,
        Enemy,
    }

    public EntityType type;
}
