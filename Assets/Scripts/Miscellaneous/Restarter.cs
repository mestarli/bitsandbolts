using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlaySong("game-over");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level_01");
        }*/
    }

    public void startGame()
    {
        AudioManager.Instance.PlaySong("btn_click");
        SceneManager.LoadScene("SelectOption");
    }
    public void quitGame()
    {
        AudioManager.Instance.PlaySong("btn_click");
        Application.Quit();
    }
}
