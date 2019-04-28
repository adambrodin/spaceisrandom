using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    public GameObject[] enemies;
    public float minCooldown, maxCooldown, difficulty = 0.1f;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        GameController.ChangeDifficulty += ChangeDifficulty;
    }

    private void ChangeDifficulty(float value)
    {
        difficulty *= value;
    }

    private GameObject RandomEnemy()
    {
        GameObject e = enemies[Random.Range(0, enemies.Length)];

        Vector3 spawnPos = transform.position;
        spawnPos.x += Random.Range(-transform.position.x / 2, transform.position.x / 2);

        e.transform.position = spawnPos;

        return e;
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(Random.Range(minCooldown - difficulty, maxCooldown - difficulty));

        if (Debug.isDebugBuild) print("Current Min: " + (minCooldown - difficulty).ToString() + "s Current Max: " + (maxCooldown - difficulty).ToString() + "s");

        GameObject enemy = RandomEnemy();

        Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);

        StartCoroutine(SpawnEnemies());
    }
}
