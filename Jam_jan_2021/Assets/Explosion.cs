using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem part;
    private SphereCollider _collider;

    void OnEnable()
    {
        part = GetComponent<ParticleSystem>();
        if(!_collider)
            _collider = GetComponent<SphereCollider>();
        _collider.enabled = false;
    }

    public IEnumerator explode()
    {
        _collider.enabled = true;
        float targetRadius = 20f, currRadius = 0f;
        float time = 0.0f;
        while (time < 0.5f)
        {
            _collider.radius = currRadius;
            time += Time.fixedDeltaTime;
            currRadius = Mathf.SmoothStep(0, targetRadius, time / 0.5f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        time = 0.0f;
        currRadius = 20f;
        while (time < 0.5f)
        {
            _collider.radius = currRadius;
            time += Time.fixedDeltaTime;
            currRadius = Mathf.SmoothStep(targetRadius, 0, time / 0.5f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        _collider.radius = 20f;
        yield return new WaitForSeconds(0.05f);
        _collider.enabled = false;
    }
}
