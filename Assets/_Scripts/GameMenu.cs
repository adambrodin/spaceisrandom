using UnityEngine;
using UnityEngine.SceneManagement;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class GameMenu : MonoBehaviour
{
    public void Restart() => SceneManager.LoadScene("Level");
}
