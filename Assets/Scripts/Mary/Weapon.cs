using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
    public string type;
    [SerializeField] private float distance;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isRotating;
    [SerializeField] private GameObject _player;
    public bool isAttacking = false;
    
    [SerializeField] private float boomerangTimer;
    [SerializeField] private  bool returning = false;
    
    [SerializeField] private float timeToRespawn;

    private bool WeaponFacing = false;
    // Start is called before the first frame update
    void Start()
    {
        boomerangTimer = 0.0f;
        _player = GameObject.FindObjectOfType<Player>().gameObject;
        startPosition = _player.transform.localPosition;
        if (_player.GetComponent<Player>().facingRight)
        {
            WeaponFacing = true;
        }
        Debug.Log(startPosition);
        switch (type)
        {
            case "axe":
                damage = 4;
                distance = 3;
                timeToRespawn = 0.8f;
                break;
            case "dagger":
                damage = 2;
                distance = 6;
                timeToRespawn = 0.2f;
                break;
            case "boomerang":
                damage = 3;
                distance = 4;
                timeToRespawn = 0.8f;
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

            StartCoroutine(reactiveAttact());
        }
    }
    

    private void AxeAttack()
    {
        if (WeaponFacing)
        {
            endPosition= new Vector2(_player.transform.localPosition.x + distance, _player.transform.localPosition.y);
        }
        else
        {
            endPosition= new Vector2(_player.transform.localPosition.x - distance, _player.transform.localPosition.y);

        }
        
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
        if (WeaponFacing)
        {
            endPosition= new Vector2(_player.transform.localPosition.x + distance, _player.transform.localPosition.y);
        }
        else
        {
            endPosition= new Vector2(_player.transform.localPosition.x - distance, _player.transform.localPosition.y);

        }
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
        if (boomerangTimer== 0)
        {
            returning = true;
        }
        if (!returning)
        {
            if (WeaponFacing)
            {
                endPosition= new Vector2(_player.transform.localPosition.x + distance, _player.transform.localPosition.y);
            }
            else
            {
                endPosition= new Vector2(_player.transform.localPosition.x - distance, _player.transform.localPosition.y);

            }
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
        
        yield return new WaitForSeconds(timeToRespawn);
        isAttacking = false;
        returning = false;
        _player.gameObject.GetComponent<Player>().canAttack = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().TakeDmg(damage);
        }
        if (collision.GetComponent<Player>() && returning)
        {
            _player.gameObject.GetComponent<Player>().canAttack = true;
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") && type != "boomerang")
        {
            _player.gameObject.GetComponent<Player>().canAttack = true;
            Destroy(gameObject);
        }
    }
}
