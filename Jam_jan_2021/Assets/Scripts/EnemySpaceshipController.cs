using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpaceshipController : ShipController
{
    private bool m_EnemyIsInShootingRange = false;

    private ShootingController m_ShootingController;
    private Collider[] m_GameObjsInShootingRange;
    private List<GameObject> m_GameObjsInGame;
    private Rigidbody rb;

    [SerializeField] private string enemyTag = "";
    [SerializeField] private float shootingRangeSphereRadius = 10.0f;
    [SerializeField] private float playerOffsetSphereRadius = 5.0f;

    protected override void IntializeStuff()
    {
        base.IntializeStuff();
        m_GameObjsInGame = new List<GameObject>(GameObject.FindGameObjectsWithTag(enemyTag).ToList());
        m_GameObjsInGame.Add(GameObject.FindGameObjectWithTag("Player"));

        m_ShootingController = GetComponent<ShootingController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameObjsInGame == null)
            return;

        m_GameObjsInShootingRange = Physics.OverlapSphere(gameObject.transform.position, shootingRangeSphereRadius);

        GameObject nearestEnemy = null;

        if (m_GameObjsInShootingRange.Length == 1)
        {
            nearestEnemy = FindClosestEnemy(m_GameObjsInGame);
            m_EnemyIsInShootingRange = false;
        }
        else
        {
            nearestEnemy = FindClosestEnemy(m_GameObjsInShootingRange);
            m_EnemyIsInShootingRange = true;
        }

        if (nearestEnemy == null)
            return;

        float distance = (nearestEnemy.transform.position - gameObject.transform.position).magnitude;

        if (distance > playerOffsetSphereRadius)
            ThrustForward(accel);

        RotateTowardsNearestEnemy(nearestEnemy);
        ClampVelocity();

        if (m_EnemyIsInShootingRange && !FireCooldown)
            m_ShootingController.Shoot(bulletKey, bulletSpeed, BulletCooldown(), 5, Experience);

        if (distance <= playerOffsetSphereRadius)
        {
            RotateTowardsNearestEnemy(nearestEnemy);
            rb.drag = 20.0f;
        }
        else
        {
            rb.drag = 0.5f;
        }
    }

    private GameObject FindClosestEnemy(object gos)
    {
        float min = 999999.0f;
        GameObject nearestEnemy = null;

        if (gos is Collider[] coll)
        {
            for (int i = 0; i < coll.Length; i++)
            {
                if (coll[i].gameObject.tag != enemyTag && coll[i].gameObject.tag != "Player")
                    continue;

                float distance = (coll[i].transform.position - gameObject.transform.position).magnitude;

                if (distance < min)
                {
                    min = distance;
                    nearestEnemy = coll[i].gameObject;
                }
            }
        }
        else if (gos is List<GameObject> goList)
        {
            foreach (var go in goList)
            {
                if (go == gameObject)
                    continue;

                float distance = (go.transform.position - gameObject.transform.position).magnitude;

                if (distance < min)
                {
                    min = distance;
                    nearestEnemy = go;
                }
            }
        }

        return nearestEnemy;
    }

    private void RotateTowardsNearestEnemy(GameObject nearestEnemy)
    {
        Vector3 direction = (nearestEnemy.transform.position - transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.z).normalized;
        Debug.DrawRay(transform.position, direction * 10f, Color.red);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
