/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System.Collections.Generic;
using TMPro;
using UnityEngine;
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]-
public class Highscore
{
    public List<HighscoreEntry> entryList;
}

[System.Serializable]
public class HighscoreEntry
{
    public int score;
    public string name;
}

public class HighscoreTable : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float entryHeightOffset;
    private Transform entryContainer, entryTemplate;

    private List<HighscoreEntry> entryList;
    private List<Transform> entryTransforms;

    [SerializeField]
    private int maxHighscoreEntries;

    private static HighscoreTable instance;
    public static HighscoreTable Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HighscoreTable>();
            }
            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        try
        {
            entryContainer = GameObject.Find("HighscoreEntryContainer").transform;
            entryTemplate = entryContainer.Find("HighscoreEntry");
            entryTemplate.gameObject.SetActive(false);
            GameObject.Find("HighscoreTable").SetActive(false);
        }
        catch { }

        // Default values
        if (PlayerPrefs.GetString("Highscores") == null || PlayerPrefs.GetString("Highscores") == "")
        {
            List<HighscoreEntry> tempList = new List<HighscoreEntry>(maxHighscoreEntries);
            for (int i = 0; i < maxHighscoreEntries; i++)
            {
                tempList.Add(new HighscoreEntry { score = 0, name = "-" });
            }

            Highscore tempHs = new Highscore();
            tempHs.entryList = tempList;
            PlayerPrefs.SetString("Highscores", JsonUtility.ToJson(tempHs));
            PlayerPrefs.Save();
        }
        entryTransforms = new List<Transform>();
    }

    public void Sort()
    {
        Highscore hs = JsonUtility.FromJson<Highscore>(PlayerPrefs.GetString("Highscores"));
        entryList = hs.entryList;

        for (int i = 0; i < entryList.Count; i++)
        {
            for (int x = i + 1; x < entryList.Count; x++)
            {
                if (entryList[x].score > entryList[i].score)
                {
                    HighscoreEntry temp = entryList[i];
                    entryList[i] = entryList[x];
                    entryList[x] = temp;
                }
            }
        }
    }

    public void Spawn()
    {
        Sort();

        // Spawn new table
        for (int i = 0; i < maxHighscoreEntries; i++)
        {
            if (maxHighscoreEntries > entryList.Count) { return; }
            AddTableEntry(entryList[i], entryContainer, entryTransforms);
        }
    }

    public HighscoreEntry getPositionInformation(int index)
    {
        Sort();
        return new HighscoreEntry { score = entryList[index].score, name = entryList[index].name };
    }

    public void AddHighscoreEntry(int score, string name)
    {
        Highscore hs = JsonUtility.FromJson<Highscore>(PlayerPrefs.GetString("Highscores"));
        HighscoreEntry entry = new HighscoreEntry { score = score, name = name };
        hs.entryList.Add(entry);
        PlayerPrefs.SetString("Highscores", JsonUtility.ToJson(hs));
        PlayerPrefs.Save();
    }

    private void AddTableEntry(HighscoreEntry entry, Transform container, List<Transform> transformList)
    {
        try
        {
            if (transformList.Count < maxHighscoreEntries)
            {
                Transform newEntry = Instantiate(entryTemplate, container);
                RectTransform entryPosition = newEntry.GetComponent<RectTransform>();

                float newYPos = entryPosition.position.y - (entryHeightOffset * transformList.Count);
                entryPosition.anchoredPosition = new Vector2(entryPosition.position.x, newYPos);

                newEntry.gameObject.SetActive(true);
                if (newEntry.Find("Position").GetComponent<TextMeshProUGUI>() != null) newEntry.Find("Position").GetComponent<TextMeshProUGUI>().text = (transformList.Count + 1).ToString();
                if (newEntry.Find("Score").GetComponent<TextMeshProUGUI>() != null) newEntry.Find("Score").GetComponent<TextMeshProUGUI>().text = entry.score.ToString();
                if (newEntry.Find("Name").GetComponent<TextMeshProUGUI>() != null) newEntry.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.name;

                transformList.Add(newEntry);
            }
        }
        catch { }
    }
}
