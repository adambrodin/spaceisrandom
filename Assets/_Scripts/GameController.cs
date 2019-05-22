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

    [SerializeField]
    private float backgroundFadeInTime, backgroundFadeOutTime, backgroundFadeDelay;
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
        StartCoroutine(FlashHealthStatus(Color.green, 3));

        Health.Instance.OnPlayerHit += PlayerHit;
    }

    private IEnumerator FlashHealthStatus(Color color, int amountOfFlashes)
    {
        // Flash green three times to indicate three hp (full)
        for (int i = 0; i < amountOfFlashes; i++)
        {
            StartCoroutine(BackgroundFade(backgroundFadeInTime, backgroundFadeOutTime, color, backgroundFadeDelay / 2));
            yield return new WaitForSeconds(backgroundFadeInTime + backgroundFadeOutTime + backgroundFadeDelay / 2);
        }
    }

    private void PlayerHit(float health)
    {
        switch (health)
        {
            case 2:
                StartCoroutine(BackgroundFade(backgroundFadeInTime, backgroundFadeOutTime, Color.yellow, backgroundFadeDelay));
                break;
            case 1:
                StartCoroutine(BackgroundFade(backgroundFadeInTime, backgroundFadeOutTime, Color.red, backgroundFadeDelay));
                break;
            case 0:
                StartCoroutine(FlashHealthStatus(Color.white, 3));
                GameOver();
                break;
        }
    }

    private IEnumerator BackgroundFade(float timeToFadeIn, float timeToFadeOut, Color targetColor, float delay)
    {
        for (float time = 0f; time < timeToFadeIn; time += 0.01f)
        {
            Camera.main.backgroundColor = Color.Lerp(Color.black, targetColor, time / timeToFadeIn);
            yield return null;
        }
        if (delay > 0) yield return new WaitForSeconds(delay);
        for (float time = 0f; time < timeToFadeOut; time += 0.01f)
        {
            Camera.main.backgroundColor = Color.Lerp(targetColor, Color.black, time / timeToFadeOut);
            yield return null;
        }
    }

    public void ObjectKilled(GameObject obj)
    {
        int killReward = (int)obj.GetComponent<IKillable<float>>().KillReward;
        if (killReward > 0) ChangeScore(killReward);

        // Destroy the object
        Destroy(obj);
    }

    private void GameOver() => OnGameOver?.Invoke();
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

