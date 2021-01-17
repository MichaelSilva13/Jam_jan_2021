using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{

    private LevelManager _levelManager;
    private Text victoryBanner;

    private Button playAgainButton;
    private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        victoryBanner = GameObject.Find("Victory Banner").GetComponent<Text>();
        if (LevelManager.playerVictory == true)
        {
            victoryBanner.text = "YOU'VE WON";
        }
        else
        {
            victoryBanner.text = "YOU'VE LOST";
        }

        playAgainButton = GameObject.Find("Play Again Button").GetComponent<Button>();
        playAgainButton.onClick.AddListener(ClickPlayAgain);

        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();
        exitButton.onClick.AddListener(ClickExit);
    }

    private void ClickPlayAgain()
    {
        //SceneManager.LoadScene(2);
    }

    private void ClickExit()
    {
        Application.Quit();
    }
}
