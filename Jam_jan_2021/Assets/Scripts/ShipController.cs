using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody rb;

    public float maxVelocity = 45f;

    public float rotationSpeed = 3f;

    public float accel = 30f;

    protected ShootingController ShootingController;
    
    public int bulletDamage;
    
    private RespawnController _respawnController;

    public float bulletCooldownTime = 0.15f, bulletSpeed = 75f, bombCooldownTime = 5f, bombSpeed = 75f;
    protected bool FireCooldown, BombFireCooldown;

    [SerializeField]
    protected string bulletKey = "Bullet", bombKey = "Bomb";

    protected Experience Experience;

    protected Health Health;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ShootingController = GetComponent<ShootingController>();
        _respawnController = GetComponent<RespawnController>();
        Health = GetComponent<Health>();
        Experience = GetComponent<Experience>();
    }


    protected void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float z = Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity);

        rb.velocity = new Vector3(x, rb.velocity.y, z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet") && !other.CompareTag("Explosion"))
        {
            Health.Kill(null);
        }
    }

    protected void ThrustForward(float amount)
    {
        Vector3 force = transform.forward * (Mathf.Clamp(amount, 0, 1) * accel);

        rb.AddForce(force);
    }

    protected void Rotate(Transform t, float amount)
    {
        t.Rotate(0, amount, 0);
    }
    
    protected IEnumerator BulletCooldown()
    {
        FireCooldown = true;
        yield return new WaitForSeconds(bulletCooldownTime);
        FireCooldown = false;
    }
    
    protected IEnumerator BombCooldown()
    {
        BombFireCooldown = true;
        yield return new WaitForSeconds(bombCooldownTime);
        BombFireCooldown = false;
    }

}
