using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Clase que hará las peticiones de JSON y devolverá los datos
/// </summary>
public class WebRequestManager : MonoBehaviour
{
    public static WebRequestManager Instance;
    
    public Users data_users;


    private void Awake()
    {
        Instance = this;
    }

    public void LeerJSON_Usuarios(bool crearListaUsuarios)
    {
        StartCoroutine(CorrutinaLeerJSON_USUARIOS(crearListaUsuarios));
    }

    public void LeerJSON_Usuarios_Y_Crear_Usuario()
    {
        StartCoroutine(CorrutinaLeerJSON_USUARIOS_Y_CREAR_USAURIO());
    }
    
    public void EscribirJSON(Users data_users)
    {
        StartCoroutine(CorrutinaEscribirJSON(data_users));
    }
    
    /// <summary>
    /// Para inicializar el fichero de json a través de context menu
    /// </summary>
    /// 
    [ContextMenu("CREA JSON RECORDS")]
    public void CrearEstructuraUSERS()
   {
            StartCoroutine(CorrutinaEscribirJSON(data_users));
   }
    
    
    /// <summary>
    /// Método para leer el archivo JSON de la URL especificada en la clase CONSTANTES
    /// Una vez se recuperan los datos se crean los 
    /// </summary>
    /// <param name="crearListaUsuarios">Este parámetro indica si se tiene que crear una lista de botones correspondiente a
    /// a cada usuario dentro del JSON</param>
    /// <returns></returns>
    private IEnumerator CorrutinaLeerJSON_USUARIOS(bool crearListaUsuarios)
    {
        UnityWebRequest web = UnityWebRequest.Get(Constants.URL_FILE_USUARIOS_JSON + Constants.NOMBRE_FILE_USERS + Constants.SUFIJO_FILE);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            data_users = JsonUtility.FromJson<Users>(web.downloadHandler.text);
            DataManager.Instance.DataUsers = data_users;

            if (crearListaUsuarios)
            {
                //DataManager.Instance.CrearUsuariosBtn();
            }
        }
        else
        {
            Debug.Log(Constants.MENSAJE_ERROR);
        }
    }
    
    /// <summary>
    /// Método para leer los usuarios que están en el JSON y crear uno nuevo
    /// </summary>
    /// <returns></returns>
    private IEnumerator CorrutinaLeerJSON_USUARIOS_Y_CREAR_USAURIO()
    {
        UnityWebRequest web = UnityWebRequest.Get(Constants.URL_FILE_USUARIOS_JSON + Constants.NOMBRE_FILE_USERS + Constants.SUFIJO_FILE);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            data_users = JsonUtility.FromJson<Users>(web.downloadHandler.text);
            DataManager.Instance.DataUsers = data_users;
            DataManager.Instance.CreateNewUser();
        }
        else
        {
            Debug.Log(Constants.MENSAJE_ERROR);
        }
    }
    
    /// <summary>
    /// Método para escribir en el fichero JSON de la nube los usuarios que existen
    /// </summary>
    /// <param name="usuarios_data">Clase Usuarios que tenemos que </param>
    /// <returns></returns>
    private IEnumerator CorrutinaEscribirJSON(Users usuarios_data)
    {
        WWWForm form = new WWWForm();
        form.AddField(Constants.PARAM_FILE, Constants.NOMBRE_FILE_USERS);
        form.AddField(Constants.PARAM_TEXT, JsonUtility.ToJson(usuarios_data));
        
        UnityWebRequest web = UnityWebRequest.Post(Constants.URL_FILE_TO_WRITE, form);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log(Constants.MENSAJE_ERROR);
        }
    }
}
