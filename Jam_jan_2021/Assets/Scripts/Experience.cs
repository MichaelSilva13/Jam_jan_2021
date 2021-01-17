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

    [SerializeField]
    private TextMesh _textMesh, lvlUpText;

    public int speedLevel = 1;
    public int fireRateLevel = 1;
    public int damageLevel = 1;

    private ShipController _shipController;

    private Health _health;
    public LevelManager _levelManager;

    [SerializeField]
    private AudioSource xpUpSound, lvlUpSound;

    private void Start()
    {
        _shipController = GetComponent<ShipController>();
        _health = GetComponent<Health>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    public int Xp
    {
        get => xp;
        set
        {
            if(CompareTag("Player"))
                StartCoroutine(ShowXpText(value));
            xp = value;
        }
    }

    public int Kills
    {
        get => kills;
        set
        {
            kills = value;
            if(gameObject == GameObject.FindGameObjectWithTag("Player"))
            {
                _levelManager.killUp(this, true);
            }
            else
            {
                _levelManager.killUp(this, false);
            }
        }
    }

    public List<int> xpLevel;

    public void LevelUp()
    {
        bool levelUp = false;
        while (xp >= xpLevel[level])
        {
            level++;
            BoostStat(Random.Range(0, 4));
            levelUp = true;
        }

        if (levelUp && lvlUpText)
            StartCoroutine(ShowLvlUpText());
    }

    private void BoostStat(int index)
    {
        switch (index)
        {
            case 0:
                _shipController.accel += 5f;
                _shipController.maxVelocity += 5f;
                speedLevel++;
                break;
            case 1:
                _health.MaxLife += 5;
                _health.Life = _health.MaxLife;
                break;
            case 2:
                _shipController.bulletCooldownTime *= 0.8f;
                _shipController.bombCooldownTime *= 0.8f;
                fireRateLevel++;
                break;
            default:
                _shipController.bulletDamage += 5;
                damageLevel++;
                break;
        }
    }

    IEnumerator ShowXpText(int value)
    {
        xpUpSound.Play();
        int gain = value - xp;
        _textMesh.text = "+" + gain + "xp";
        yield return new WaitForSeconds(2f);
        _textMesh.text = "";
    }

    IEnumerator ShowLvlUpText()
    {
        lvlUpSound.Play();
        _textMesh.text = "LVL UP!";
        yield return new WaitForSeconds(3f);
        _textMesh.text = "";
    }
}
