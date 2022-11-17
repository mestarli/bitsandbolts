using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySong("bg_menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnStart()
    {
        SceneManager.LoadScene("SelectOption");
    }
    public void BtnExit()
    {
        Application.Quit();
    }
}
