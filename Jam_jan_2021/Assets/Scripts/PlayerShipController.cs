using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : ShipController
{
    private void FixedUpdate()
    {
        float zAxis = !Health.Dead ? Input.GetAxis("Vertical") : 0;
        float xAxis = !Health.Dead ? Input.GetAxis("Horizontal") : 0;

        ThrustForward(zAxis);
        Rotate(transform, xAxis * rotationSpeed);
        ClampVelocity();
    }
    
    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1") && !FireCooldown && !Health.Dead)
        {
            ShootingController.Shoot(bulletKey, bulletSpeed, BulletCooldown(), bulletDamage, Experience);
        }
        
        if (Input.GetButtonDown("Fire2") && !BombFireCooldown && !Health.Dead)
        {
            ShootingController.Shoot(bombKey, bombSpeed, BombCooldown(), bulletDamage * 2, Experience);
        }
    }
}
