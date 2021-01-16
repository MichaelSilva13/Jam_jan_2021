using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public void Shoot(string projKey, float speed, IEnumerator routine)
    {
        StartCoroutine(routine);
        GameObject proj = GameObjectPoolController.Dequeue(projKey).gameObject;
        proj.transform.rotation = transform.rotation;
        proj.SetActive(true);
        proj.transform.position = transform.position + (transform.forward * 2f);
        proj.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}
