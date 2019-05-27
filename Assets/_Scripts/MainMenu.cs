/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the next scene in order
    public void PlayButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void QuitButton() => Application.Quit();
}
