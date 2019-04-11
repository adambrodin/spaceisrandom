using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    public GameObject[] enemies;
    #endregion

    private void Start()
    {
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        Instantiate(enemies[0], gameObject.transform.position, enemies[0].transform.rotation);

        StartCoroutine(spawnEnemies());
    }
}
