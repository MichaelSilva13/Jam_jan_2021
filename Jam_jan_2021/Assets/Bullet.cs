using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float sdTime = 1f;
    void OnEnable()
    {
        StartCoroutine(SelfDestruct());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(sdTime);
        GameObjectPoolController.Enqueue(GetComponent<Poolable>());
    }
}
