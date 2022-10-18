using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    
    private User new_user_tmp;
    [SerializeField] private Users data_users;
    
    /// <summary>
    /// Gets y setters
    /// </summary>
    public User NewUserTMP
    {
        get =>  new_user_tmp;
        set =>  new_user_tmp = value;
    }
    public Users DataUsers
    {
        get => data_users;
        set => data_users = value;
    }
    
    private void Awake()
    {
        Instance = this;
    }
    
    /// <summary>
    /// Método que crea el usuario
    /// </summary>
    public void CreateNewUser()
    {
        List<User> users_list_tmp = new List<User>();

        if (data_users != null)
        {
            foreach (var usuario in data_users.users_records)
            {
                users_list_tmp.Add(usuario);
            }
        }
        
        users_list_tmp.Add(new_user_tmp);
        if (data_users != null)
        {
            data_users.users_records = users_list_tmp.ToArray();
            WebRequestManager.Instance.EscribirJSON(data_users);
        }
    }
}
