using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : UnityEngine.MonoBehaviour
{
    #region Variables
    public GameObject bullet;
    public Bullet stats;
    private bool canFire = true;
    #endregion

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
