/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using System.Collections;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject[] pickups;
    [SerializeField]
    private float minCooldown, maxCooldown, startSpawnDelay, boundsOffset;
    #endregion

    private void Start()
    {
        GameController.Instance.OnGameStart += GameStart;
        GameController.Instance.OnGameOver += GameOver;
    }

    private void GameStart() => StartCoroutine(GameStartDelay());
    private void GameOver() => StopAllCoroutines();
    public void PickedUp() => StartCoroutine(SpawnPickup());

    private IEnumerator GameStartDelay()
    {
        yield return new WaitForSeconds(startSpawnDelay);
        StartCoroutine(SpawnPickup());
    }

    private IEnumerator SpawnPickup()
    {
        GameObject randomPickup = RandomPickup();
        yield return new WaitForSeconds(UnityEngine.Random.Range(minCooldown, maxCooldown));
        Instantiate(randomPickup, randomPickup.transform.position, randomPickup.transform.rotation);
    }

    private GameObject RandomPickup()
    {

        if (pickups.Length > 0)
        {
            GameObject gObj;
            do
            {
                gObj = pickups[UnityEngine.Random.Range(0, pickups.Length)];
            } while (gObj.name.Contains("HealthPickup") && Player.Instance.GetComponent<Health>().CurrentHealth >= 5);

            float randomX = UnityEngine.Random.Range(GameController.Instance.bounds.xMin + boundsOffset, GameController.Instance.bounds.xMax - boundsOffset);
            float randomZ = UnityEngine.Random.Range(GameController.Instance.bounds.zMin + boundsOffset, GameController.Instance.bounds.zMax - boundsOffset);
            Vector3 randomPos = new Vector3(randomX, gObj.transform.position.y, randomZ);
            gObj.transform.position = randomPos;

            if (gObj != null) { return gObj; }
        }

        return null;

    }
}
