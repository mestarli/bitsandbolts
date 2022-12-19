using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
            else
            {
              
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
        }
    }

    public void resumeGame()
    {
        AudioManager.Instance.PlaySong("btn_click");
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void restartGame()
    {
        AudioManager.Instance.PlaySong("btn_click");
        SceneManager.LoadScene("StartMenu");
    }

    public void exitGame()
    {
        AudioManager.Instance.PlaySong("btn_click");
        Application.Quit();
    }
}
