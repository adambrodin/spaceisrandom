using System;
using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class GameController : MonoBehaviour
{
    #region Variables
    public TMPro.TextMeshProUGUI scoreText;
    [SerializeField]
    private int score = 0;
    public static event Action<float> ChangeDifficulty;
    public float increaseDifficultyTime, minIncrease, maxIncrease;
    #endregion

    private void Start()
    {
        Health.EntityKilled += OnKill;
        scoreText.text = "";

        StartCoroutine(IncreaseDifficulty());
    }

    void OnKill(GameObject obj)
    {
        ChangeScore((int)obj.GetComponent<Health>().stats.killReward);

        Destroy(obj);
    }

    void ChangeScore(int value)
    {
        score += value;

        scoreText.text = score.ToString();
    }

    private IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(increaseDifficultyTime);
        ChangeDifficulty?.Invoke(UnityEngine.Random.Range(minIncrease, maxIncrease));

        StartCoroutine(IncreaseDifficulty());
    }
}

