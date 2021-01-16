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
    }
    
    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1") && !_fireCooldown)
        {
            _shootingController.Shoot(bulletKey, bulletSpeed, BulletCooldown(), bulletDamage, _experience);
        }
        
        if (Input.GetButtonDown("Fire2") && !_bombFireCooldown)
        {
            _shootingController.Shoot(bombKey, bombSpeed, BombCooldown(), bulletDamage * 2, _experience);
        }
    }
}
