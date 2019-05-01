using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class Enemy : EntityBase
{
    private void OnTriggerEnter(Collider col)
    {
        if (col != null && col.gameObject.tag == "Player")
        {
            col.GetComponent<Health>().TakeDamage(1);

            Destroy(gameObject);
        }
    }
}
