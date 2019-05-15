using System;
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
        GameController.Instance.OnChangeDifficulty += ChangeDifficulty;
        GameController.Instance.OnGameOver += GameOver;
    }

    private void ChangeDifficulty(float value)
    {
        difficulty += value;
    }

    private void GameOver()
    {
        // Destroy self, stop spawning enemies
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in enemiesInScene)
        {
            try
            {
                Destroy(g.transform.parent.gameObject);
            }
            catch (Exception)
            {
                Destroy(g);
            }
        }

        Destroy(gameObject);
    }

    private GameObject RandomEnemy()
    {
        if (enemies.Length > 0)
        {
            GameObject e;
            do
            {
                e = enemies[UnityEngine.Random.Range(0, enemies.Length)];
            } while (e.gameObject == null);

            Vector3 spawnPos = transform.position;
            spawnPos.x += UnityEngine.Random.Range(transform.position.x - spawnMaxOffset, transform.position.x + spawnMaxOffset);

            e.transform.position = spawnPos;

            return e;
        }
        return null;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minCooldown - difficulty, maxCooldown - difficulty));

        GameObject enemy = RandomEnemy();
        Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);

        StartCoroutine(SpawnEnemies());
    }
}
