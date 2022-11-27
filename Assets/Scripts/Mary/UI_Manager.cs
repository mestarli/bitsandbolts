using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    // Recogemos los paneles para habilitar o deshabilitar
    [SerializeField] private GameObject panel_presentacion;
    [SerializeField] private GameObject panel_developer;
    [SerializeField] private GameObject panel_texture;
    [SerializeField] private GameObject panel_animation;
    [SerializeField] private GameObject panel_modeler;
    [SerializeField] private GameObject panel_controles;
    
    [SerializeField] private GameObject animation_01;
    [SerializeField] private GameObject animation_02;
    
    // Guardamos los valores en 1 o 2, para las diferentes opciones elegidas
    [SerializeField] private int phantomVersion = 0;
    [SerializeField] private int spiderVersion = 0;
    [SerializeField] private int skeletonVersion = 0;
    [SerializeField] private int batVersion = 0;
    [SerializeField] private int textureSelection = 0;
    [SerializeField] private int animationSelection = 0;
    [SerializeField] private int modelerSelection = 0;
    
    public static UI_Manager  Instance { get; private set; }
    private int selection = 0;
    
    public int PhantomVersion()
    {
        return phantomVersion;
    }
    public int BatVersion()
    {
        return batVersion;
    }
    public int SkeletonVersion()
    {
        return skeletonVersion;
    }
    public int SpiderVersion()
    {
        return spiderVersion;
    }
    public int TextureVersion()
    {
        return textureSelection;
    }
    
    public int ModelerSelection()
    {
        return modelerSelection;
    }
    public int AnimationSelection()
    {
        return animationSelection;
    }


    void Awake()
    {
        if (panel_developer != null)
        {
            panel_presentacion.SetActive(true);
            panel_developer.SetActive(false);
            panel_modeler.SetActive(false);
            panel_texture.SetActive(false);
            panel_animation.SetActive(false);
            panel_controles.SetActive(false);
        }
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        AudioManager.Instance.PlaySong("bg-music");
    }
    

    public void ChangeSelector(GameObject selector)
    {
        selector.transform.GetChild(0).gameObject.SetActive(!selector.transform.GetChild(0).gameObject.active);
        selector.transform.GetChild(1).gameObject.SetActive(!selector.transform.GetChild(1).gameObject.active);
    }

    public void ChangePanel(int level)
    {
        AudioManager.Instance.PlaySong("btn_click");
        switch (level)
        {
            case 0:
                panel_presentacion.SetActive(false);
                panel_developer.SetActive(true);
                break;
            case 1:
                if (GameObject.FindWithTag("option_01") != null)
                {
                    selection = 1;
                    AddOptionSelected(level,selection);
                }
                if (GameObject.FindWithTag("option_02") != null)
                {
                    selection = 2;
                    AddOptionSelected(level,selection);
                }
                if (GameObject.FindWithTag("option_03") != null)
                {
                    selection = 1;
                    AddOptionSelected(6,selection);
                }
                if (GameObject.FindWithTag("option_04") != null)
                {
                    selection = 2;
                    AddOptionSelected(6,selection);
                }
                if (GameObject.FindWithTag("option_05") != null)
                {
                    selection = 1;
                    AddOptionSelected(7,selection);
                }
                if (GameObject.FindWithTag("option_06") != null)
                {
                    selection = 2;
                    AddOptionSelected(7,selection);
                }
                if (GameObject.FindWithTag("option_07") != null)
                {
                    selection = 1;
                    AddOptionSelected(5,selection);
                }
                if (GameObject.FindWithTag("option_08") != null)
                {
                    selection = 2;
                    AddOptionSelected(5,selection);
                }
                panel_developer.SetActive(false);
                panel_modeler.SetActive(true);
                break;
            case 2:
                if (GameObject.FindWithTag("option_01") != null)
                {
                    selection = 1;
                }
                if (GameObject.FindWithTag("option_02") != null)
                {
                    selection = 2;
                }
                panel_modeler.SetActive(false);
                panel_texture.SetActive(true);
                AddOptionSelected(level,selection);
                break;
            case 3:
                if (GameObject.FindWithTag("option_01") != null)
                {
                    selection = 1;
                }
                if (GameObject.FindWithTag("option_02") != null)
                {
                    selection = 2;
                }

                if (modelerSelection == 2)
                {
                    animation_01.SetActive(false);
                    animation_02.SetActive(true);
                }
                if (modelerSelection == 1)
                {
                    animation_01.SetActive(true);
                    animation_02.SetActive(false);
                }
                panel_animation.SetActive(true);
                panel_texture.SetActive(false);
                AddOptionSelected(level,selection);
                break;
            case 4:
                if (GameObject.FindWithTag("option_01") != null)
                {
                    selection = 1;
                }
                if (GameObject.FindWithTag("option_02") != null)
                {
                    selection = 2;
                }
                panel_animation.SetActive(false);
                panel_controles.SetActive(true);
                AddOptionSelected(level,selection);
                break;
            case 5:
                SceneManager.LoadScene("Level_01");
                break;
            default:
                Debug.Log("No has pasado ningun nivel");
                break;
        }
    }

    private void AddOptionSelected(int level, int selection)
    {
        switch (level)
        {
            case 1:
                Debug.Log("Has elegido la version del fantasma");
                phantomVersion = selection;
                break;
            case 5:
                Debug.Log("Has elegido la version de spider");
                spiderVersion = selection;
                break;
            case 6:
                Debug.Log("Has elegido la version de skeleton");
                skeletonVersion = selection;
                break;
            case 7:
                Debug.Log("Has elegido la version de batman");
                batVersion = selection;
                break;
            case 2:
                Debug.Log("Has elegido las opciones de modelado");
                modelerSelection = selection;
                break;
            case 3:
                Debug.Log("Has elegido las opciones de texturizado");
                textureSelection = selection;
                break;
            case 4:
                Debug.Log("Has elegido las opciones de animacion");
                animationSelection = selection;
                break;
            default:
                skeletonVersion= 1;
                batVersion= 1;
                spiderVersion= 1;
                phantomVersion= 1;
                textureSelection = 1;
                animationSelection = 1;
                modelerSelection = 1;
                break;
        }
    }
}
