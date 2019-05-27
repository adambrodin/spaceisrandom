/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void Restart() => SceneManager.LoadScene("Level");
}
