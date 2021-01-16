using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private List<GameObject> m_GameObjsInGame;

    private List<GameObject> respawnBeacons;

    public string enemyTag1;
    public string enemyTag2;

    // Start is called before the first frame update
    void Start()
    {
        m_GameObjsInGame = new List<GameObject>();
        m_GameObjsInGame.Add(GameObject.FindGameObjectWithTag(enemyTag1));
        m_GameObjsInGame.Add(GameObject.FindGameObjectWithTag(enemyTag2));
        m_GameObjsInGame.Add(GameObject.FindGameObjectWithTag("Player"));

        respawnBeacons = new List<GameObject>(GameObject.FindGameObjectsWithTag("RespawnBeacon").ToList());
    }

    public Vector3 GetFurthestRespawnBecon(GameObject deadShip)
    {
        float furthestDistance = -1.0f;

        GameObject furthestBeacon = respawnBeacons[0]; 

        foreach(var beacon in respawnBeacons)
        {
            List<float> ennemyDistance = new List<float>();

            foreach(var ship in m_GameObjsInGame)
            {
                if(ship != deadShip)
                {
                    ennemyDistance.Add((ship.transform.position - beacon.transform.position).magnitude);
                }
            }

            ennemyDistance.Sort();

            if(ennemyDistance[0] > furthestDistance)
            {
                furthestBeacon = beacon;
                furthestDistance = ennemyDistance[0];
            }
        }

        return furthestBeacon.transform.position;

    }
}
