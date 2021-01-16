using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
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

    private void Explode()
    {
        GameObjectPoolController.Enqueue(GetComponent<Poolable>());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(sdTime);
        Explode();
    }
}
