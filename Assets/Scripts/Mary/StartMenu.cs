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
        StartCoroutine(btnStart());
    }
    public void BtnExit()
    {
        StartCoroutine(btnExit());
    }

    IEnumerator btnStart()
    {
        AudioManager.Instance.PlaySong("btn_click");
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("SelectOption");
    }
    IEnumerator btnExit()
    {
        AudioManager.Instance.PlaySong("btn_click");
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }
}
