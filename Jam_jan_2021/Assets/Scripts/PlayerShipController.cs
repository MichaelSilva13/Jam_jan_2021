using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : ShipController
{
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
        if (Input.GetButtonDown("Fire1") && !FireCooldown)
        {
            ShootingController.Shoot(bulletKey, bulletSpeed, BulletCooldown(), bulletDamage, Experience);
        }
        
        if (Input.GetButtonDown("Fire2") && !BombFireCooldown)
        {
            ShootingController.Shoot(bombKey, bombSpeed, BombCooldown(), bulletDamage * 2, Experience);
        }
    }
}
