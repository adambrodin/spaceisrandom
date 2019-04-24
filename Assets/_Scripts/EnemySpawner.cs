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
    private GameObject playerObj;

    public float spawnCooldown;
    #endregion

    private void Awake()
    {
        playerObj = GameObject.Find("Player");
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnCooldown);

        int randomEnemy = Random.Range(0, enemies.Length);
        float xPos = transform.position.x;
        Vector3 randomSpawn = new Vector3(xPos += Random.Range(-5, 5), transform.position.y, transform.position.z);

        Instantiate(enemies[randomEnemy], randomSpawn, enemies[randomEnemy].transform.rotation);

        StartCoroutine(SpawnEnemies());
    }
}
