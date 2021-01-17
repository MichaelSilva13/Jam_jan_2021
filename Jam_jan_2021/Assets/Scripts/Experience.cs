using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Experience : MonoBehaviour
{
    [SerializeField]
    private int xp;

    private int kills;

    [SerializeField]
    private int level;

    private ShipController _shipController;

    private Health _health;
    private LevelManager _levelManager;

    private void Start()
    {
        _shipController = GetComponent<ShipController>();
        _health = GetComponent<Health>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    public int Xp
    {
        get => xp;
        set => xp = value;
    }

    public int Kills
    {
        get => kills;
        set
        {
            kills = value;
            _levelManager.killUp(this);
        }
    }

    public List<int> xpLevel;

    public void LevelUp()
    {
        while (xp > xpLevel[level])
        {
            level++;
            BoostStat(Random.Range(0, 4));
        }
    }

    private void BoostStat(int index)
    {
        Debug.Log(index);
        switch (index)
        {
            case 0:
                _shipController.accel += 5f;
                _shipController.maxVelocity += 5f;
                break;
            case 1:
                _health.MaxLife += 5;
                _health.Life = _health.MaxLife;
                break;
            case 2:
                _shipController.bulletCooldownTime *= 0.8f;
                _shipController.bombCooldownTime *= 0.8f;
                break;
            default:
                _shipController.bulletDamage += 5;
                break;
        }
    }
}
