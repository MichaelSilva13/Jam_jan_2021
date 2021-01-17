using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipController : ShipController
{
    private Text damageText;
    private Text fireRateText;
    private Text speedText;
    private Text killText;

    private void Start()
    {
        damageText = GameObject.Find("Damage Text").GetComponent<Text>();
        fireRateText = GameObject.Find("FireRate Text").GetComponent<Text>();
        speedText = GameObject.Find("Speed Text").GetComponent<Text>();
        killText = GameObject.Find("Kill Text").GetComponent<Text>();
        IntializeStuff();
    }

    private void FixedUpdate()
    {
        float zAxis = !Health.Dead ? Input.GetAxis("Vertical") : 0;
        float xAxis = !Health.Dead ? Input.GetAxis("Horizontal") : 0;

        ThrustForward(zAxis);
        Rotate(transform, xAxis * rotationSpeed);
        ClampVelocity();

        damageText.text = "Lv.  " + Experience.damageLevel;
        fireRateText.text = "Lv.  " + Experience.fireRateLevel;
        speedText.text = "Lv.  " + Experience.speedLevel;
        killText.text = Experience.Kills + "    KILLS";
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
