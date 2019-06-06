/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
#pragma warning disable CS0649 // Disable incorrect warning caused by private field with [SerializeField]
public class GameMenu : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject highscoreTable, gameoverPanel;
    [SerializeField]
    private float changeSceneWaitTime;
    #endregion

    public void HoverSound() => FindObjectOfType<AudioManager>().SetPlaying("SelectedSound", true);
    public void ClickSound() => FindObjectOfType<AudioManager>().SetPlaying("ClickedSound", true);
    public void Restart() => StartCoroutine(LoadSceneWait("Level", changeSceneWaitTime));
    public void MainMenu() => StartCoroutine(LoadSceneWait("MainMenu", changeSceneWaitTime));
    public void Quit() => System.Diagnostics.Process.GetCurrentProcess().Kill();
    private IEnumerator LoadSceneWait(string name, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(name);
    }

    private void Start() => EventSystem.current.SetSelectedGameObject(GameObject.Find("PlayButton"), new BaseEventData(EventSystem.current));
    public void HighscoreViewer()
    {
        if (!highscoreTable.activeSelf)
        {
            gameoverPanel.SetActive(false);
            highscoreTable.SetActive(true);
            HighscoreTable.Instance.Sort();
            HighscoreTable.Instance.Spawn();
            EventSystem.current.SetSelectedGameObject(GameObject.Find("BackButton"), new BaseEventData(EventSystem.current));
        }
        else
        {
            gameoverPanel.SetActive(true);
            highscoreTable.SetActive(false);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("RestartButton"), new BaseEventData(EventSystem.current));
        }
    }

}
