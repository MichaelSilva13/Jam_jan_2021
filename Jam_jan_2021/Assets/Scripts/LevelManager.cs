using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int killThreshold;

    public static bool playerVictory;

    public void killUp(Experience e, bool isPlayer)
    {
        if (e.Kills >= killThreshold)
        {
            playerVictory = isPlayer;
            if (isPlayer)
            {
                Debug.Log("GAGNÉ!!");
            }
            else
            {
                Debug.Log("Lol loser");
            }
            SceneManager.LoadScene(1);
        }
    }
}
