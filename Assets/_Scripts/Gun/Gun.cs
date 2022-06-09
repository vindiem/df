using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;

    float timeBtwShots;
    public float startTimeBtwShots;

    public AudioSource shotSound;

    // ammo
    [HideInInspector] public int ammo = 20;
    [HideInInspector] public int maxAmmo = 20;

    // ammo show()
    public Image ammoBar;

    private void Update()
    {
        // Limit of ammo set
        if (ammo > maxAmmo)
        {
            ammo--;
        }
        
        // ammo instantiate
        ammoBar.fillAmount = (float)ammo / (float)maxAmmo;

        // rotate gun
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && ammo > 0)
            {
                ammoChange(-1);
                shotSound.Play();
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }

            // reload
            if ((Input.GetKeyDown(KeyCode.R) && ammo <= 10) || (ammo <= 2))
            {
                StartCoroutine(WaitAnimTimeSecs(.35f));
            }
        }

        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void ammoChange(int value) // 2 usage in this script
    {
        ammo += value;
    }

    IEnumerator WaitAnimTimeSecs(float secs)
    {
        for (int i = ammo; i <= maxAmmo; i++)
        {
            ammoChange(1);
            yield return new WaitForSeconds(secs);
        }
        
    }
}