using System;
using System.Collections;
using UnityEngine;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
[Serializable]
public class EnemyObject
{
    // The object to spawn
    public GameObject gObject;
    // Spawn chance (0-100%)
    public int spawnChance;
}

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    public EnemyObject[] enemies;
    public float minCooldown, maxCooldown, spawnMaxOffset;
    private float difficulty = 0;
    private static System.Random randomizer;
    #endregion

    private void Start()
    {
        GameController.Instance.OnChangeDifficulty += ChangeDifficulty;
        GameController.Instance.OnGameOver += GameOver;
        StartCoroutine(SpawnEnemies());
    }

    private void ChangeDifficulty(float value) => difficulty += value;

    private void GameOver()
    {
        // Destroy self, stop spawning enemies
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemiesInScene)
        {
            try
            {
                Destroy(enemy.transform.parent.gameObject);
            }
            catch (Exception)
            {
                Destroy(enemy);
            }
        }

        Destroy(gameObject);
    }

    private bool ChanceChecker(int chance)
    {
        if (randomizer == null) { randomizer = new System.Random(); }

        if (randomizer.Next(100) < chance)
        {
            return true;
        }

        return false;
    }

    private GameObject RandomEnemy()
    {
        if (enemies.Length > 0)
        {
            GameObject gameObj = null;
            foreach (EnemyObject enemyObj in enemies)
            {
                if (ChanceChecker(enemyObj.spawnChance))
                {
                    gameObj = enemyObj.gObject;
                    break;
                }
            }

            if (gameObj != null)
            {
                Vector3 spawnPos = transform.position;
                spawnPos.x += UnityEngine.Random.Range(transform.position.x - spawnMaxOffset, transform.position.x + spawnMaxOffset);

                gameObj.transform.position = spawnPos;
                return gameObj;
            }
        }

        // If the enemy array is empty
        return null;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minCooldown - difficulty, maxCooldown - difficulty));
        try
        {
            GameObject enemy = RandomEnemy();
            Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
        }
        catch (Exception) { }
        StartCoroutine(SpawnEnemies());
    }
}

