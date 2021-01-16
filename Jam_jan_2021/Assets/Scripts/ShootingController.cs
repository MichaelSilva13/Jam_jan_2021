using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public void Shoot(string projKey, float speed, IEnumerator routine, int damage, Experience source)
    {
        StartCoroutine(routine);
        GameObject proj = GameObjectPoolController.Dequeue(projKey).gameObject;
        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.Damage = damage;
        projectile.User = source;
        proj.transform.rotation = transform.rotation;
        proj.SetActive(true);
        proj.transform.position = transform.position + (transform.forward * 4f);
        proj.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}
