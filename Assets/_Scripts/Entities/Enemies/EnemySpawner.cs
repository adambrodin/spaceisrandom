using System;
using System.Collections;
using UnityEngine;
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
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
    public int spawnChance, maxSpawnOffset; // Max offset defaults to x screen bounds 
}

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private EnemyObject[] enemies;
    [SerializeField]
    private float difficulty, minCooldown, maxCooldown, enemyGroupOffset;
    [SerializeField]
    private int enemiesInGroupMin, enemiesInGroupMax;
    private static System.Random randomizer;
    #endregion

    private void Start()
    {
        GameController.Instance.OnChangeDifficulty += ChangeDifficulty;
        GameController.Instance.OnGameStart += GameStart;
        GameController.Instance.OnGameOver += GameOver;
    }

    private void GameStart() { StartCoroutine(SpawnEnemies()); }
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
            GameObject gameObj = default;
            int objMaxSpawnOffset = default;

            while (gameObj == null)
            {
                foreach (EnemyObject enemyObj in enemies)
                {
                    if (ChanceChecker(enemyObj.spawnChance))
                    {
                        gameObj = enemyObj.gObject;
                        objMaxSpawnOffset = enemyObj.maxSpawnOffset;
                        break;
                    }
                }
            }
            if (gameObj != null)
            {
                Vector3 spawnPos = transform.position;
                spawnPos.x += UnityEngine.Random.Range(transform.position.x - objMaxSpawnOffset, transform.position.x + objMaxSpawnOffset);

                gameObj.transform.position = spawnPos;
                return gameObj;
            }
        }

        // If the enemy array is empty
        if (Debug.isDebugBuild) { print("No random enemy was picked, defaulting to [0]"); }
        return enemies[0].gObject;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minCooldown - difficulty, maxCooldown - difficulty));
        GameObject enemy = RandomEnemy();

        switch (enemy.name)
        {
            case "Enemy_Turret_Group":
                // How many enemies the group should spawn
                int numberOfEnemies = UnityEngine.Random.Range(enemiesInGroupMin, enemiesInGroupMax);
                // Calculate a random targetZ value that doesen't differ too much (to keep the group together)
                float targetZ = GameController.Instance.bounds.zMin + UnityEngine.Random.Range(enemyGroupOffset, GameController.Instance.bounds.zMax - enemyGroupOffset);
                // Placeholder variables to keep track of how far the enemy has travelled and spawning positions
                float spawnPosX = 0, spawnPosZ = 0;

                // Get the current position, spawn an enemy add offset and repeat
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Vector3 spawnPos = new Vector3(spawnPosX, transform.position.y, transform.position.z);
                    GameObject spawnedObj = Instantiate(enemy, spawnPos, enemy.transform.rotation);
                    spawnPosZ = (int)spawnedObj.transform.position.z;
                    // Set the target of the turrets destination to this targetZ (so the group sticks together)
                    spawnedObj.GetComponent<TurretMovement>().targetZ = targetZ;
                    // Don't spawn another enemy until there's a minimum position offset between them (to stop them from spawning inside eachother)
                    yield return new WaitUntil(() => (spawnPosZ - spawnedObj.transform.position.z) >= enemyGroupOffset);
                    // Add offset
                    spawnPosX += (UnityEngine.Random.Range(-enemyGroupOffset, enemyGroupOffset));
                }
                break;
            default:
                // Spawn a enemy the normal way
                Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
                break;
        }
        StartCoroutine(SpawnEnemies());
    }
}

