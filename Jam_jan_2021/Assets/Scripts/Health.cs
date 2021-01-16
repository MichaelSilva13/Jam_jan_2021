using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private float blinkTime;
    private int life;

    private Collider _collider;
    private MeshRenderer _meshRenderer;

    public int Life
    {
        get => life;
        set => life = value;
    }

    public int MaxLife
    {
        get => maxLife;
        set => maxLife = value;
    }
    
    [SerializeField]
    private float invinsibilityTime = 1f;

    private Experience _experience;

    // Start is called before the first frame update
    void Start()
    {
        _experience = GetComponent<Experience>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _collider = GetComponentInChildren<Collider>();
        life = MaxLife;
    }

    public void Damage(int value, Experience experience)
    {
        life -= value;
        Debug.Log(life);
        if (life < 0)
        {
            Kill(experience);
        }
        else
        {
            StartCoroutine(Invinsibility());
        }
    }

    public void Kill(Experience experience)
    {
        if (experience && experience != _experience)
        {
            _experience.LevelUp();
            experience.Xp += 5;
        }
        //TODO: Add respawn system
    }

    IEnumerator Invinsibility()
    {
        _collider.enabled = false;
        float timer = 0;
        while (timer < invinsibilityTime)
        {
            _meshRenderer.enabled = !_meshRenderer.enabled;
            timer += blinkTime;
            yield return new WaitForSeconds(blinkTime);
        }

        _meshRenderer.enabled = true;
        _collider.enabled = true;
    }

}
