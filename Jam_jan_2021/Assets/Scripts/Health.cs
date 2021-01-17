using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private float blinkTime;
    private int life;

    [SerializeField]
    private AudioSource _audioSource;
    public bool Dead
    {
        get;
        set;
    }

    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private RespawnController _respawnController;
    private Rigidbody rb;

    public Healthbar healthBar;

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
        _respawnController = GetComponent<RespawnController>();
        rb = GetComponent<Rigidbody>();
        life = MaxLife;
        if(healthBar)
            healthBar.SetMaxHealth(life);
    }

    public void Damage(int value, Experience experience)
    {
        life -= value;
        if(healthBar)
            healthBar.SetHealth(life);
        if (life <= 0)
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
        _audioSource.Play();
        if (experience && experience != _experience)
        {
            _experience.Xp += 5;
            _experience.LevelUp();
            experience.Xp += 10;
            experience.Kills++;
        }

        Dead = true;
        StartCoroutine(DeathAnim());
    }

    void Respawn()
    {
        Dead = false;
        transform.position = _respawnController.GetFurthestRespawnBecon(gameObject);
        transform.LookAt(Vector3.zero);
        life = maxLife;
        if(healthBar)
            healthBar.SetMaxHealth(life);
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

    IEnumerator DeathAnim()
    {
        rb.velocity = Vector3.zero;
        _meshRenderer.enabled = false;
        _collider.enabled = false;
        GameObject explosion = GameObjectPoolController.Dequeue("BOOM").gameObject;
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
        GameObjectPoolController.Enqueue(explosion.GetComponent<Poolable>());
        _meshRenderer.enabled = true;
        _collider.enabled = true;
        Respawn();
    }

}
