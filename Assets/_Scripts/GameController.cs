using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class GameController : MonoBehaviour
{
    #region Variables
    int score = 0;
    #endregion

    private void Start()
    {
        Health.entityKilled += OnKill;
    }

    void OnKill(GameObject obj)
    {
        Destroy(obj);

        score += (int)obj.GetComponent<Health>().stats.killReward;
        print("Score: " + score);
    }
}
