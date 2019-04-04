using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            Instantiate(bullet, transform.position, transform.rotation);

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
