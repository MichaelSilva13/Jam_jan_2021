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
    private RespawnController _respawnController;
    private Rigidbody rb;

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

        StartCoroutine(DeathAnim());
    }

    void Respawn()
    {
        gameObject.transform.position = _respawnController.GetFurthestRespawnBecon(gameObject);
        life = maxLife;
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
