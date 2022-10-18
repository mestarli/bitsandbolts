using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    // Recogemos los paneles para habilitar o deshabilitar
    [SerializeField] private GameObject panel_developer;
    [SerializeField] private GameObject panel_texture;
    [SerializeField] private GameObject panel_animation;
    [SerializeField] private GameObject panel_modeler;
    
    // Guardamos los valores en 1 o 2, para las diferentes opciones elegidas
    [SerializeField] private int developerSelection = 0;
    [SerializeField] private int textureSelection = 0;
    [SerializeField] private int animationSelection = 0;
    [SerializeField] private int modelerSelection = 0;

    private static UI_Manager Instance;
    private int selection = 0;
    
    void Awake()
    {
        panel_developer.SetActive(true);
        panel_modeler.SetActive(false);
        panel_texture.SetActive(false);
        panel_animation.SetActive(false);
    }
    

    public void ChangeSelector(GameObject selector)
    {
        Debug.Log("Hello");
        selector.transform.GetChild(0).gameObject.SetActive(!selector.transform.GetChild(0).gameObject.active);
        selector.transform.GetChild(1).gameObject.SetActive(!selector.transform.GetChild(1).gameObject.active);
    }

    public void ChangePanel(int level)
    {
        switch (level)
        {
            case 1:
                if (GameObject.FindWithTag("option_01") != null)
                {
                    selection = 1;
                }
                if (GameObject.FindWithTag("option_02") != null)
                {
                    selection = 2;
                }
                AddOptionSelected(level,selection);
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
                Debug.Log("Ya deberías pasar al nivel principal");
                AddOptionSelected(level,selection);
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
                Debug.Log("Has elegido las opciones de programacion");
                developerSelection = selection;
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
                developerSelection = 1;
                textureSelection = 1;
                animationSelection = 1;
                modelerSelection = 1;
                break;
        }
    }
}
