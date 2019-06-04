/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI numberOneText;

    public void HoverSound() => FindObjectOfType<AudioManager>().SetPlaying("SelectedSound", true);
    public void ClickSound() => FindObjectOfType<AudioManager>().SetPlaying("ClickedSound", true);

    // Loads the next scene in order
    public void PlayButton() => StartCoroutine(LoadSceneWait("Level", 0.5f));
    private IEnumerator LoadSceneWait(string name, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(name);
    }
    public void QuitButton() => Application.Quit();

    private void Start()
    {
        FindObjectOfType<AudioManager>().SetPlaying("AmbientSounds", true);
        HighscoreTable table = FindObjectOfType<HighscoreTable>();
        table.Sort();
        int score = table.getPositionInformation(0).score;
        string name = table.getPositionInformation(0).name;
        numberOneText.text = $"#1 - <color=#00ffffff>{name} - <color=#FFD700>{score}";
    }
}
