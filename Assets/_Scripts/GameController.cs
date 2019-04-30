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
        StartCoroutine(IncreaseDifficulty());
    }

    void OnKill(GameObject obj)
    {
        int killReward = (int)obj.GetComponent<EntityBase>().getStats().killReward;
        if (killReward > 0) ChangeScore(killReward);

        Destroy(obj);
    }

    // Removes the event delegate to save performance
    private void OnDisable()
    {
        Health.EntityKilled -= OnKill;
    }

    private void OnEnable()
    {
        Health.EntityKilled += OnKill;
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

