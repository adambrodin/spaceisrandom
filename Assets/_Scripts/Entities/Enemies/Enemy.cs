using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class Enemy : EntityBase
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col != null) col.GetComponent<Health>().TakeDamage(1);

            Destroy(gameObject);
        }
    }
}
