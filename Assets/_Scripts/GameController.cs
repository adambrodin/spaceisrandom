using System;
using System.Collections;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[Serializable]
public class RandomColorRange
{
    public float hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin = 1f, alphaMax = 1f;
}

[Serializable]
public class Bounds
{
    public float xMin, xMax, zMin, zMax;
}

public class GameController : MonoBehaviour
{
    #region Variables
    public TMPro.TextMeshProUGUI scoreText;
    private static GameController instance;
    public event Action<float> ChangeDifficulty;
    public RandomColorRange randomColorRange;
    public float increaseDifficultyTime, minIncrease, maxIncrease;
    [SerializeField]
    private int score;
    public Bounds bounds;
    #endregion

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameController)) as GameController;
            }
            return instance;
        }
    }

    private void Start()
    {
        // Hide the cursor on release build
        if (!Debug.isDebugBuild)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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

    public void ChangeScore(int value)
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

