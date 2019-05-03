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
    public float minCooldown, maxCooldown, spawnMaxOffset;
    private float difficulty = 0;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        GameController.Instance.ChangeDifficulty += ChangeDifficulty;
    }

    private void ChangeDifficulty(float value)
    {
        difficulty += value;
    }

    private GameObject RandomEnemy()
    {
        if (enemies.Length > 0)
        {
            GameObject e = enemies[Random.Range(0, enemies.Length)];

            Vector3 spawnPos = transform.position;
            spawnPos.x += Random.Range(transform.position.x - spawnMaxOffset, transform.position.x + spawnMaxOffset);

            e.transform.position = spawnPos;

            return e;
        }
        return null;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(Random.Range(minCooldown - difficulty, maxCooldown - difficulty));

        GameObject enemy = RandomEnemy();

        Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);

        StartCoroutine(SpawnEnemies());
    }
}
