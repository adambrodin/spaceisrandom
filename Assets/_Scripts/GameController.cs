using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class GameController : MonoBehaviour
{
    #region Variables
    #endregion

    private void Start()
    {
        Health.entityKilled += OnKill;
    }

    void OnKill(GameObject obj)
    {
        print(obj.name + " died.");
    }
}
