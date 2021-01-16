using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody rb;

    public float maxVelocity = 3f;

    public float rotationSpeed = 3f;

    public float accel = 3f;

    protected ShootingController _shootingController;
    
    public int bulletDamage;
    
    [SerializeField]
    private GameObject bullet, bomb;
    
    
    public float bulletCooldownTime = 0.5f, bulletSpeed = 75f, bombCooldownTime = 5f, bombSpeed = 75f;
    protected bool _fireCooldown, _bombFireCooldown;

    [SerializeField]
    protected string bulletKey = "Bullet", bombKey = "Bomb";

    protected Experience _experience;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _shootingController = GetComponent<ShootingController>();
        _experience = GetComponent<Experience>();
    }


    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float z = Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity);

        rb.velocity = new Vector3(x, rb.velocity.y, z);

    }

    protected void ThrustForward(float amount)
    {
        Vector3 force = transform.forward * (amount * accel);

        rb.AddForce(force);
    }

    protected void Rotate(Transform t, float amount)
    {
        t.Rotate(0, amount, 0);
    }
    
    protected IEnumerator BulletCooldown()
    {
        _fireCooldown = true;
        yield return new WaitForSeconds(bulletCooldownTime);
        _fireCooldown = false;
    }
    
    protected IEnumerator BombCooldown()
    {
        _bombFireCooldown = true;
        yield return new WaitForSeconds(bombCooldownTime);
        _bombFireCooldown = false;
    }
}
