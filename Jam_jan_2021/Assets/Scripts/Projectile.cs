using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Projectile : MonoBehaviour
{
    protected Experience user;

    public Experience User
    {
        get => user;
        set => user = value;
    }

    public int Damage
    {
        get;
        set;
    }
}
