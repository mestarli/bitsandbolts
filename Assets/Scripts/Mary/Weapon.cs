using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage;
    public string type;
    [SerializeField] private float distance;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isRotating;
    [SerializeField] private GameObject _player;
    [SerializeField] private bool isAttacking;
    
    [SerializeField] private float boomerangTimer;
    [SerializeField] private  bool returning = false;
    // Start is called before the first frame update
    void Start()
    {
        boomerangTimer = 0.0f;
        isAttacking = false;
        startPosition = transform.localPosition;
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

    void Update()
    {
        if(isAttacking)
        {
            SelfRotation();
            switch (this.type)
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

            StartCoroutine(reactiveAttact());
        }
    }

    public void WeaponAttack()
    {
        isAttacking = true;
    }

    private void AxeAttack()
    {
        isRotating = false;
        endPosition= new Vector2(_player.transform.localPosition.x + distance, transform.localPosition.y);
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, endPosition, moveSpeed * Time.deltaTime);
        boomerangTimer =  endPosition.x - transform.localPosition.x;
        if (boomerangTimer  == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    private void DaggerAttack()
    {
        isRotating = false;
        endPosition= new Vector2(_player.transform.localPosition.x + distance, transform.localPosition.y);
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, endPosition, moveSpeed * Time.deltaTime);
        boomerangTimer =  endPosition.x - transform.localPosition.x;
        if (boomerangTimer  == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    private void BoomerangAttack()
    {
        isRotating = true;
        boomerangTimer =  endPosition.x - transform.localPosition.x;
        if (boomerangTimer  == 0)
        {
            returning = true;
        }
        if (!returning)
        {
            endPosition= new Vector2(_player.transform.localPosition.x + distance, transform.localPosition.y);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, endPosition, moveSpeed * Time.deltaTime);
             
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPosition, moveSpeed * Time.deltaTime);

        }
    }

    private void SelfRotation()
    {
        if (isRotating)
        {
            transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0,0,0);
        }
    }
    
    IEnumerator reactiveAttact()
    {
        
        yield return new WaitForSeconds(0.8f);
        transform.localPosition = startPosition;
        isAttacking = false;
        returning = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _player.gameObject.GetComponent<Player>().canAttack = true;
    }

}
