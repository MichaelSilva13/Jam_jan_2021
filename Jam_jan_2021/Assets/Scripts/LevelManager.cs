using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int killThreshold;
    public void killUp(Experience e, bool isPlayer)
    {
        if (e.Kills > killThreshold)
        {
            if (isPlayer)
            {
                Debug.Log("GAGNÉ!!");
            }
            else
            {
                Debug.Log("Lol loser");
            }
            
        }
    }
}
