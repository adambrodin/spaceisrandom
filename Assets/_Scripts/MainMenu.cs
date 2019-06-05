/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI numberOneText;
    [SerializeField]
    private GameObject mainMenu, optionsMenu, volumeSlider;

    public void HoverSound() => FindObjectOfType<AudioManager>().SetPlaying("SelectedSound", true);
    public void ClickSound() => FindObjectOfType<AudioManager>().SetPlaying("ClickedSound", true);

    public void MainMenuButton()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("PlayButton"), new BaseEventData(EventSystem.current));
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("VolumeSlider"), new BaseEventData(EventSystem.current));
    }

    // Loads the next scene in order
    public void PlayButton() => StartCoroutine(LoadSceneWait("Level", 0.5f));
    private IEnumerator LoadSceneWait(string name, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(name);
    }
    public void QuitButton() => Application.Quit();

    public void VolumeSlider()
    {
        PlayerPrefs.SetFloat("GlobalVolume", volumeSlider.GetComponent<Slider>().value);
        PlayerPrefs.Save();
        AudioManager.Instance.UpdateSounds();
    }

    private void Start()
    {
        // Hide and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("GlobalVolume", 1.0f);
        FindObjectOfType<AudioManager>().SetPlaying("AmbientSounds", true);
        HighscoreTable table = FindObjectOfType<HighscoreTable>();
        table.Sort();
        int score = table.getPositionInformation(0).score;
        string name = table.getPositionInformation(0).name;
        numberOneText.text = $"#1 - <color=#00ffffff>{name} - <color=#FFD700>{score}";
    }
}
