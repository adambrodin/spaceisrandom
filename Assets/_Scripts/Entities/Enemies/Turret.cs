using UnityEngine;

public class Turret : EntityBase
{
    private void OnTriggerEnter(Collider col)
    {
        // If collision with a Player object is found
        if (col != null && col.gameObject.tag == "Player")
        {
            col.GetComponent<IKillable<float>>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
