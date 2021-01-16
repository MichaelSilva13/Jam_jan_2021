using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField]
    private float sdTime = 1f, explosionTime;

    private bool exploding;
    private AudioSource _audioSource;

    private MeshRenderer _meshRenderer;
    private ParticleSystem explosionParticles;
    private Rigidbody _rigidbody;
    private Explosion _explosion;
    private Coroutine detonateRoutine;
    private Collider _collider;

    void OnEnable()
    {
        if (!_meshRenderer)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        if (!explosionParticles)
        {
            explosionParticles = GetComponentInChildren<ParticleSystem>();
        }

        if (!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        if (!_explosion)
        {
            _explosion = GetComponentInChildren<Explosion>();
        }

        if (!_audioSource)
            _audioSource = GetComponent<AudioSource>();

        if (!_collider)
            _collider = GetComponent<Collider>();

        _audioSource.Play();
        _meshRenderer.enabled = true;
        _collider.enabled = true;
        exploding = false;
        explosionParticles.Stop();
        detonateRoutine = StartCoroutine(Detonate());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!exploding)
        {
            StopCoroutine(detonateRoutine);
            Explode();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Explode()
    {
        exploding = true;
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        _rigidbody.velocity = Vector3.zero;
        explosionParticles.Play();
        StartCoroutine(SelfDestruct());
        _explosion.Damage = Damage;
        _explosion.User = user;
        StartCoroutine(_explosion.explode());
        //GameObjectPoolController.Enqueue(GetComponent<Poolable>());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(explosionTime);
        GameObjectPoolController.Enqueue(GetComponent<Poolable>());
    }

    IEnumerator Detonate()
    {
        yield return new WaitForSeconds(sdTime);
        Explode();
    }
}
