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
    [HideInInspector] public float ammo = 20f;

    // ammo show()
    public Image ammoBar;
    [HideInInspector] public float maxAmmo = 20f;

    private void Update()
    {
        // ammo instantiate
        ammoBar.fillAmount = ammo / maxAmmo;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && ammo > 0)
            {
                ammo--;
                shotSound.Play();
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }

            else if (Input.GetKeyDown(KeyCode.R) || ammo <= 0)
            {
                StartCoroutine(WaitAnimTimeSecs(2.5f));
            }
        }

        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void ammoMinus(int value)
    {
        ammo += value;
    }

    IEnumerator WaitAnimTimeSecs(float secs)
    {
        yield return new WaitForSeconds(secs);
        ammo = maxAmmo;
    }
}
