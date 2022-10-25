using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject[] Life;
    [SerializeField] private GameObject[] WeaponSelected;
    // Start is called before the first frame update
    public static UIGame instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void UpdateLife(int heartPosition)
   {
       Life[heartPosition].GetComponent<SpriteRenderer>().color = new Color(150, 133, 133);
   }

    public void UpdateWeapon(int weaponPosition)
    {
        
        foreach (var weapon in WeaponSelected)
        {
            weapon.SetActive(false);   
        }
        WeaponSelected[weaponPosition].SetActive(true);
    }
}
