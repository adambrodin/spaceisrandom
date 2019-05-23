using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class MainMenu : MonoBehaviour
{
    // Loads the next scene in order
    public void PlayButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void QuitButton() => Application.Quit();
}
