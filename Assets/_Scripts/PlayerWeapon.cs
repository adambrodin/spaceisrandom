using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region Variables
    public GameObject bullet;
    public Bullet stats;
    private bool canFire = true;
    private TextMeshProUGUI scoreText;
    #endregion

    private void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>(); ///////////////// REMOVE TODO
        scoreText.text = "0"; ////////////////////// REMOVE TODO
    }

    void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if(Input.GetKey(KeyCode.Space) && canFire)
        {
            GameObject g = Instantiate(bullet, transform.position, transform.rotation);

            g.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        canFire = false;

        yield return new WaitForSeconds(stats.projectileCooldown);

        canFire = true;
    }
}
