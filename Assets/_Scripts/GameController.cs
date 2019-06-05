using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public TextMeshProUGUI scoreText;
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

    [SerializeField]
    private Color[] healthIndicator;
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
        Health.Instance.OnPlayerHit += PlayerHit;

        AudioManager.Instance.SetPlaying("BackgroundMusic", true);
        AudioManager.Instance.SetPlaying("AmbientSounds", true);
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

    private IEnumerator FlashHealthStatus(Color targetColor, bool inOut, float timeToFadeIn, float timeToFadeOut, float delay, float volume, int amountOfFlashes)
    {
        StopCoroutine(BackgroundFade(targetColor, inOut, timeToFadeIn, timeToFadeOut, delay));

        // Flash green three times to indicate three hp (full)
        for (int i = 0; i < amountOfFlashes; i++)
        {
            StartCoroutine(BackgroundFade(targetColor, inOut, timeToFadeIn, timeToFadeOut, delay));
            AudioManager.Instance.Set("HealthIndicator", AudioManager.Instance.GetSound("HealthIndicator").pitch, volume);
            AudioManager.Instance.SetPlaying("HealthIndicator", true);
            yield return new WaitForSeconds(timeToFadeIn + timeToFadeOut + delay);
        }
    }

    private void PlayerHit(float health) => HealthBlinker(health);
    private void HealthBlinker(float health)
    {
        switch (health)
        {
            case 5:
                StartCoroutine(FlashHealthStatus(healthIndicator[5], true, 0.07f, 0.07f, 0.07f, 1f, 5));
                break;
            case 4:
                StartCoroutine(FlashHealthStatus(healthIndicator[4], true, 0.1f, 0.1f, 0.1f, 1f, 4));
                break;
            case 3:
                StartCoroutine(FlashHealthStatus(healthIndicator[3], true, 0.15f, 0.15f, 0.15f, 1f, 3));
                break;
            case 2:
                StartCoroutine(FlashHealthStatus(healthIndicator[2], true, 0.175f, 0.175f, 0.175f, 1f, 2));
                break;
            case 1:
                StartCoroutine(FlashHealthStatus(healthIndicator[1], true, 0.075f, 0.075f, 0.075f, 1f, 5));
                break;
            case 0:
                StartCoroutine(FlashHealthStatus(healthIndicator[0], true, 1, 0.5f, 0.5f, 1f, 1));
                StartCoroutine(GameOver());
                break;
        }
    }

    private IEnumerator BackgroundFade(Color targetColor, bool inOut, float timeToFadeIn, float timeToFadeOut, float delay)
    {
        for (float time = 0f; time < timeToFadeIn; time += 0.01f)
        {
            Camera.main.backgroundColor = Color.Lerp(Color.black, targetColor, time / timeToFadeIn);
            yield return null;
        }

        if (delay > 0) { yield return new WaitForSeconds(delay); }
        if (inOut)
        {
            for (float time = 0f; time < timeToFadeOut; time += 0.01f)
            {
                Camera.main.backgroundColor = Color.Lerp(targetColor, Color.black, time / timeToFadeOut);
                yield return null;
            }
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
        AudioManager.Instance.SetPlaying("PlayerExplosion", true);
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.Set("BackgroundMusic", 1.0f, 0.1f);
        AudioManager.Instance.SetPlaying("GameOver", true);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.Set("BackgroundMusic", 1.0f, 0.45f);
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("RestartButton"), new BaseEventData(EventSystem.current));
        DestroyObjectsInScene();
        OnGameOver?.Invoke();

        // Adds a new highscore with a randomized char name/id
        string[] alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string randomName = "";
        for (int i = 0; i < UnityEngine.Random.Range(2, 6); i++) { randomName = randomName + alphabet[UnityEngine.Random.Range(0, alphabet.Length)]; }

        HighscoreTable.Instance.AddHighscoreEntry(score, randomName);
        scoreText.text = $"{scoreText.text}: <color=#00ffffff>{randomName}";
    }

    private void DestroyObjectsInScene()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) { Destroy(enemy.gameObject); }
        foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pickup")) { Destroy(pickup.gameObject); }
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

