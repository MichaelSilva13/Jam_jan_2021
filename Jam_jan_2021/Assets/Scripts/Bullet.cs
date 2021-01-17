using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField]
    private float sdTime = 1f;

    private Collider _collider;
    [SerializeField]
    private ParticleSystem _sparkParticle;

    [SerializeField]
    private AudioSource impactSound;

    private ParticleSystem _particle;
    private Coroutine SDCoroutine;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private AudioSource _audioSource;

    void OnEnable()
    {
        if (!_collider)
            _collider = GetComponent<Collider>();
        if (!_rigidbody)
            _rigidbody = GetComponent<Rigidbody>();
        if (!_particle)
            _particle = GetComponent<ParticleSystem>();
        if (!_meshRenderer)
            _meshRenderer = GetComponent<MeshRenderer>();
        if (!_audioSource)
            _audioSource = GetComponent<AudioSource>();
        
        _particle.Play();
        _sparkParticle.Stop();
        _meshRenderer.enabled = true;
        _collider.enabled = true;
        _audioSource.Play();
        SDCoroutine = StartCoroutine(SelfDestruct());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();
        if (!otherHealth)
        {
            otherHealth = other.GetComponentInParent<Health>();
        }
        if (otherHealth)
        {
            otherHealth.Damage(Damage, user);
            
        }
        StopCoroutine(SDCoroutine);
        StartCoroutine(Sparks());
    }

    IEnumerator Sparks()
    {
        impactSound.Play();
        _meshRenderer.enabled = false;
        _particle.Stop();
        _rigidbody.velocity = Vector3.zero;
        _collider.enabled = false;
        _sparkParticle.Play();
        yield return new WaitForSeconds(sdTime);
        GameObjectPoolController.Enqueue(GetComponent<Poolable>());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(sdTime);
        StartCoroutine(Sparks());
    }
}
