using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject[] Life;
    [SerializeField] private GameObject[] WeaponSelected;
    [SerializeField] private GameObject Scenario;

    // Start is called before the first frame update
    public static UIGame instance;

    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    void Start()
    {
        AudioManager.Instance.PlaySong("bg_level_01");
        try
        {
            if (UI_Manager.Instance.TextureVersion() == 1)
            {
                Scenario.transform.GetChild(0).gameObject.SetActive(true);
                Scenario.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                Scenario.transform.GetChild(0).gameObject.SetActive(false);
                Scenario.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void UpdateLife(int heartPosition)
   {
       Life[heartPosition].transform.GetChild(0).gameObject.SetActive(false);
       Life[heartPosition].transform.GetChild(1).gameObject.SetActive(true);
   }
   public void RestartLife()
   {
       Life[0].transform.GetChild(0).gameObject.SetActive(true);
       Life[0].transform.GetChild(1).gameObject.SetActive(false);
       Life[1].transform.GetChild(0).gameObject.SetActive(true);
       Life[1].transform.GetChild(1).gameObject.SetActive(false);
       Life[2].transform.GetChild(0).gameObject.SetActive(true);
       Life[2].transform.GetChild(1).gameObject.SetActive(false);
   }

    public void UpdateWeapon(int weaponPosition)
    {
        
        foreach (var weapon in WeaponSelected)
        {
            weapon.SetActive(false);   
        }
        WeaponSelected[weaponPosition].SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
