/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using System;
using System.Collections;
using System.Collections.Generic;
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
        PickupBase.OnPickup += PickedUp;
    }

    private void GameStart() => StartCoroutine(GameStartDelay());
    private void GameOver() => StopAllCoroutines();
    private void PickedUp() => StartCoroutine(SpawnPickup());

    private IEnumerator GameStartDelay()
    {
        yield return new WaitForSeconds(startSpawnDelay);
        StartCoroutine(SpawnPickup());
    }

    private IEnumerator SpawnPickup()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minCooldown, maxCooldown));
        GameObject randomPickup = RandomPickup();
        Instantiate(randomPickup, randomPickup.transform.position, randomPickup.transform.rotation);
    }

    private GameObject RandomPickup()
    {
        if (pickups.Length > 0)
        {
            GameObject gObj = pickups[UnityEngine.Random.Range(0, pickups.Length)];

            float randomX, randomZ;
            randomX = UnityEngine.Random.Range(GameController.Instance.bounds.xMin + boundsOffset, GameController.Instance.bounds.xMax - boundsOffset);
            randomZ = UnityEngine.Random.Range(GameController.Instance.bounds.zMin + boundsOffset, GameController.Instance.bounds.zMax - boundsOffset);
            Vector3 randomPos = new Vector3(randomX, gObj.transform.position.y, randomZ);
            gObj.transform.position = randomPos;

            if (gObj != null) { return gObj; }
        }

        return null;
    }
}
