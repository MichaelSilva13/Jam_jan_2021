using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAi : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ais;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ai in ais)
        {
            // Instantiate(ai);
            Vector3 pos = ai.GetComponent<RespawnController>().GetFurthestRespawnBecon(ai);
            ai.transform.position = pos;
            ai.transform.LookAt(Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
