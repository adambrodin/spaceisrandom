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
    public event Action<float> OnChangeDifficulty;
    public event Action OnGameOver;
    public RandomColorRange randomColorRange;
    public float increaseDifficultyTime, minIncrease, maxIncrease;
    public Bounds bounds;
    private int score;

    private static GameController instance;
    #endregion

    // Singleton
    public static GameController Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType<GameController>(); }
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


    public void OnKill(GameObject obj)
    {
        int killReward = (int)obj.GetComponent<IKillable<float>>().KillReward;
        if (killReward > 0) ChangeScore(killReward);

        // If the player has died, end the game
        if (obj.tag == "Player")
        {
            Destroy(obj);
            GameOver();
            return;
        }

        // Destroy the parent object if necessary (to prevent leftover models in scene)
        try
        {
            Destroy(obj.transform.parent.gameObject);
        }
        catch (Exception)
        {
            Destroy(obj);
        }

        print(obj.name + " has died");
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void ChangeScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    private IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(increaseDifficultyTime);
        OnChangeDifficulty?.Invoke(UnityEngine.Random.Range(minIncrease, maxIncrease));

        StartCoroutine(IncreaseDifficulty());
    }
}

