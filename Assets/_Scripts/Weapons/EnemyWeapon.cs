using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
public class EnemyWeapon : MonoBehaviour
{

    //TODO IMPLEMENT BETTER
    #region Variables
    public GameObject bullet;
    #endregion

    private void Start()
    {
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(0.75f);

        Instantiate(bullet, transform.position, transform.rotation);

        StartCoroutine(shoot());
    }
}
