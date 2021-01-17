using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int killThreshold;
    public void killUp(Experience e)
    {
        if (e.Kills > killThreshold)
        {
            Debug.Log("GAGNÉ!!");
        }
    }
}
