using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private Button playButton;
    private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton = GameObject.Find("Play Button").GetComponent<Button>();
        playButton.onClick.AddListener(ClickPlay);

        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();
        exitButton.onClick.AddListener(ClickExit);
    }

    private void ClickPlay()
    {
        //SceneManager.LoadScene(2);
    }

    private void ClickExit()
    {
        Application.Quit();
    }
}
