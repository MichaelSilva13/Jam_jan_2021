using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;

    public List<String> keys;

    public List<Vector2> ranges;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            GameObjectPoolController.AddEntry(keys[i], prefabs[i], (int)ranges[i].x, (int)ranges[i].y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
