using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet, bomb;

    [SerializeField]
    private float bulletCooldownTime = 0.5f, bulletSpeed = 10f, bombCooldownTime = 2.5f, bombSpeed = 15f;
    private bool fireCooldown, bombFireCooldown;

    [SerializeField]
    private string bulletKey = "PlayerBullet", bombKey = "PlayerBomb";
    // Start is called before the first frame update
    void Start()
    {
        GameObjectPoolController.AddEntry(bulletKey, bullet, 10, 20);
        GameObjectPoolController.AddEntry(bombKey, bomb, 10, 20);
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1") && !fireCooldown)
        {
            StartCoroutine(BulletCooldown());
            GameObject bullet = GameObjectPoolController.Dequeue(bulletKey).gameObject;
            bullet.SetActive(true);
            bullet.transform.position = transform.position + (transform.forward * 2f);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }
        
        if (Input.GetButtonDown("Fire2") && !bombFireCooldown)
        {
            StartCoroutine(BombCooldown());
            GameObject bomb = GameObjectPoolController.Dequeue(bombKey).gameObject;
            bomb.SetActive(true);
            bomb.transform.position = transform.position + (transform.forward * 2f);
            bomb.GetComponent<Rigidbody>().AddForce(transform.forward * bombSpeed, ForceMode.Impulse);
        }
    }


    IEnumerator BulletCooldown()
    {
        fireCooldown = true;
        yield return new WaitForSeconds(bulletCooldownTime);
        fireCooldown = false;
    }
    
    IEnumerator BombCooldown()
    {
        bombFireCooldown = true;
        yield return new WaitForSeconds(bombCooldownTime);
        bombFireCooldown = false;
    }
}
