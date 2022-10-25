using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage;
    public string type;
    [SerializeField] private float distance;
    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case "axe":
                damage = 4;
                distance = 2;
                break;
            case "dagger":
                damage = 2;
                distance = 6;
                break;
            case "boomerang":
                damage = 3;
                distance = 4;
                break;
            default:
                Debug.Log("No has definido el tipo de arma");
                break;
        }
    }

    public void WeaponAttack(string type)
    {
        switch (type)
        {
            case "axe":
                AxeAttack();
                break;
            case "dagger":
                DaggerAttack();
                break;
            case "boomerang":
                BoomerangAttack();
                break;
            default:
                Debug.Log("No has definido el tipo de arma");
                break;
        }
    }

    private void AxeAttack()
    {
        
    }
    private void DaggerAttack()
    {
        
    }
    private void BoomerangAttack()
    {
        
    }
}
