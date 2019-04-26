using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class GameController : MonoBehaviour
{
    #region Variables
    public TMPro.TextMeshProUGUI scoreText;
    private int score = 0;
    #endregion

    private void Start()
    {
        Health.entityKilled += OnKill;

        scoreText.text = "";
    }

    void OnKill(GameObject obj)
    {
        ChangeScore((int)obj.GetComponent<Health>().stats.killReward);

        Destroy(obj);
    }

    void ChangeScore(int value)
    {
        score += value;

        scoreText.text = score.ToString();
    }
}
