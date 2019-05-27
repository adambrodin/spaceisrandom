using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
[System.Serializable]
public class RandomColorRange
{
    public float hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin = 1f, alphaMax = 1f;
}
[System.Serializable]
public class Bounds
{
    public float xMin, xMax, zMin, zMax;
}

public class GameController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject gameOverPanel;
    public event Action<float> OnChangeDifficulty;
    public event Action OnGameStart, OnGameOver;
    public RandomColorRange randomColorRange;
    public Bounds bounds;
    private int score;

    [SerializeField]
    private float backgroundFadeInTime, backgroundFadeOutTime, backgroundFadeDelay, increaseDifficultyTime, minIncrease, maxIncrease;
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
        Health.Instance.OnPlayerHit += PlayerHit;

        AudioManager.Instance.SetPlaying("BackgroundMusic", true);
        StartCoroutine(StartGame());
        StartCoroutine(IncreaseDifficulty());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        HealthBlinker(3);
        yield return new WaitForSeconds(1f);
        OnGameStart?.Invoke();
    }

    private IEnumerator FlashHealthStatus(Color color, int amountOfFlashes, float timeMultiplier, float volume)
    {
        // Flash green three times to indicate three hp (full)
        for (int i = 0; i < amountOfFlashes; i++)
        {
            StartCoroutine(BackgroundFade((backgroundFadeInTime * timeMultiplier), (backgroundFadeOutTime * timeMultiplier), color, (backgroundFadeDelay / 2)));
            AudioManager.Instance.Set("HealthIndicator", AudioManager.Instance.GetSound("HealthIndicator").pitch, volume);
            AudioManager.Instance.SetPlaying("HealthIndicator", true);
            yield return new WaitForSeconds((backgroundFadeInTime * timeMultiplier) + (backgroundFadeOutTime * timeMultiplier) + (backgroundFadeDelay * timeMultiplier) / 2);
        }
    }

    private void PlayerHit(float health) => HealthBlinker(health);
    private void HealthBlinker(float health)
    {
        switch (health)
        {
            case 3:
                StartCoroutine(FlashHealthStatus(Color.green, 3, 1f, 1f));
                break;
            case 2:
                StartCoroutine(FlashHealthStatus(Color.yellow, 2, 1f, 1f));
                break;
            case 1:
                StartCoroutine(FlashHealthStatus(Color.red, 1, 1f, 1f));
                break;
            case 0:
                StartCoroutine(FlashHealthStatus(Color.white, 10, 0.2f, 1f));
                StartCoroutine(GameOver());
                break;
        }
    }

    private IEnumerator BackgroundFade(float timeToFadeIn, float timeToFadeOut, Color targetColor, float delay)
    {
        for (float time = 0f; time < timeToFadeIn; time += 0.01f)
        {
            Camera.main.backgroundColor = Color.Lerp(Color.black, targetColor, time / timeToFadeIn);
        }
        if (delay > 0) { yield return new WaitForSeconds(delay); }
        for (float time = 0f; time < timeToFadeOut; time += 0.01f)
        {
            Camera.main.backgroundColor = Color.Lerp(targetColor, Color.black, time / timeToFadeOut);
        }
    }

    public void ObjectKilled(GameObject obj)
    {
        int killReward = (int)obj.GetComponent<IKillable<float>>().KillReward;
        if (killReward > 0) { AddScore(killReward); }

        // Destroy the object
        Destroy(obj);
    }
    private IEnumerator GameOver()
    {
        AudioManager.Instance.Set("BackgroundMusic", 1.0f, 0.1f);
        AudioManager.Instance.SetPlaying("GameOver", true);
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.Set("BackgroundMusic", 1.0f, 0.8f);
        gameOverPanel.SetActive(true);
        OnGameOver?.Invoke();
        DestroyObjectsInScene();
    }

    private void DestroyObjectsInScene()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(g.gameObject);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Pickup"))
        {
            Destroy(g.gameObject);
        }
    }
    public void AddScore(int value)
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

