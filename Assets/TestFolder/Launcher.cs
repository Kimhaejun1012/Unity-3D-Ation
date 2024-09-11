using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle;
    public Transform player;

    float shotCoolTime = 3f;
    float curCoolTime = 0f;
    void Update()
    {
        if (Time.time > curCoolTime + shotCoolTime)
        {
            curCoolTime = Time.time;
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = muzzle.position;
            var dir = player.transform.position - muzzle.transform.position;
            bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * 10f, ForceMode.Impulse);
            Destroy(bullet, 10f);
        }
    }
}
