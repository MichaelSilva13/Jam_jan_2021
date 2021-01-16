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

    private ShootingController _shootingController;

    private RespawnController _respawnController;

    [SerializeField]
    private GameObject bullet, bomb;
    
    [SerializeField]
    private float bulletCooldownTime = 0.5f, bulletSpeed = 10f, bombCooldownTime = 2.5f, bombSpeed = 15f;
    private bool fireCooldown, bombFireCooldown;

    [SerializeField]
    private string bulletKey = "PlayerBullet", bombKey = "PlayerBomb";

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _shootingController = GetComponent<ShootingController>();
        _respawnController = GetComponent<RespawnController>();
        GameObjectPoolController.AddEntry(bulletKey, bullet, 10, 20);
        GameObjectPoolController.AddEntry(bombKey, bomb, 10, 20);
    }

    private void FixedUpdate()
    {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        ThrustForward(zAxis);
        Rotate(transform, xAxis * rotationSpeed);
        ClampVelocity();
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1") && !fireCooldown)
        {
            _shootingController.Shoot(bulletKey, bulletSpeed, BulletCooldown());
        }
        
        if (Input.GetButtonDown("Fire2") && !bombFireCooldown)
        {
            _shootingController.Shoot(bombKey, bombSpeed, BombCooldown());
        }
    }


    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float z = Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity);

        rb.velocity = new Vector3(x, rb.velocity.y, z);

    }

    private void ThrustForward(float amount)
    {
        Vector3 force = transform.forward * (Mathf.Clamp(amount, 0, 1) * accel);

        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, amount, 0);
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

    private void Respawn()
    {
        gameObject.transform.position = _respawnController.GetFurthestRespawnBecon(gameObject);
        rb.velocity = new Vector3(0, 0, 0);
    }
}
