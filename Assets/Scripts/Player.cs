using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    public GameObject particlesJump;

    float inputMov;
    Rigidbody2D rigidbody_;
    public float speed;
    public float jumpForce;
    public float gravityScale;
    float lastPosition=0;

    Transform checker;
    public float radiusChecker;
    public LayerMask layerMaskGround;
    bool inGround;
    bool goingUp;
    float knockback;
    public float knockbackHorizontalForce = 2;
    public float knockbackVerticalForce;
    public float tenacity = 6;

    public bool facingRight = true;
    
    [SerializeField] private int life = 3;
    [SerializeField] private GameObject ContentWeapon;
    [SerializeField] private GameObject ActiveWeapon;
    
    [SerializeField] private int positionActiveWeapon = 0;
    
    [SerializeField]private Vector2 initialPositionWeapons;
    [SerializeField]private Vector2 topPositionWeapons;
    
    public bool canAttack;

    [SerializeField] private bool topAttack;
    
    [SerializeField] private Sprite player_01_model;
    [SerializeField] private Sprite player_02_model;
    // Start is called before the first frame update
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private GameObject[] weapons;
    private void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        checker = transform.GetChild(0);
        initialPositionWeapons = new Vector2(0,0);
        topPositionWeapons = new Vector2(-0.49f,0.58f);
        canAttack = true;
        topAttack = false;

        try
        {
            if (UI_Manager.Instance.ModelerSelection() == 1)
            {
                Debug.Log("Has elegido el personaje "+UI_Manager.Instance.ModelerSelection());
                gameObject.GetComponent<SpriteRenderer>().sprite = player_01_model;
            }
            if (UI_Manager.Instance.ModelerSelection() == 2)
            {
                Debug.Log("Has elegido el personaje "+UI_Manager.Instance.ModelerSelection());
                gameObject.GetComponent<SpriteRenderer>().sprite = player_02_model;
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
        if(knockback != 0)
        {
            if (knockback > 0)
            {
                knockback -= Time.deltaTime * tenacity;
            }
            else
            {
                knockback += Time.deltaTime * tenacity;
            }
            if(knockback<0.2f && knockback > -0.2f)
            {
                knockback = 0;
            }
        }
        if (lastPosition < transform.position.y)
        {
            goingUp=true;
        }
        else
        {
            goingUp = false;
        }
        if (knockback == 0)
        {
            inputMov = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            inputMov = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && inGround) 
        {
            inGround = false;
            Jump();
        }
   
        if (!Input.GetKey(KeyCode.Space))
        {
            StopJump();
        }
        
        inGround = Physics2D.OverlapCircle(checker.position, radiusChecker, layerMaskGround);

        lastPosition = transform.position.y;
        
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("OscarScene");
        }
        
        //Press g for change weapon
        if (Input.GetKeyDown(KeyCode.Q) && canAttack)
        {

            if (positionActiveWeapon < 2)
            {
                positionActiveWeapon += 1;
            }
            else
            {
                positionActiveWeapon = 0;
            }
            
            UIGame.instance.UpdateWeapon(positionActiveWeapon);
        }

        if (Input.GetKeyDown(KeyCode.W) && canAttack)
        {
            //topAttack = true;
        }

        if (Input.GetKeyUp(KeyCode.W) && canAttack)
        {
            //topAttack = false;
        }

        if(positionActiveWeapon!=1 &&  topAttack && Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            ContentWeapon.transform.localPosition = topPositionWeapons;
            GameObject weapon = Instantiate(weapons[positionActiveWeapon], weaponPoint.position, Quaternion.identity);
            weapon.GetComponent<Weapon>().isAttacking = true;
        }
        if(!topAttack && Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            GameObject weapon = Instantiate(weapons[positionActiveWeapon], weaponPoint.position, Quaternion.identity);
            if (positionActiveWeapon == 1 && facingRight)
            {
                weapon.transform.localRotation = Quaternion.Euler(0, 0,-89.287f);
            }
            if (positionActiveWeapon == 1 && !facingRight)
            {
                weapon.transform.localRotation = Quaternion.Euler(0, 0,90.058f);
            }
            weapon.GetComponent<Weapon>().isAttacking = true;
        }

        
        
        // For flip player
        if (inputMov > 0 && !facingRight)
        {
            Flip();
        }
        if (inputMov < 0 && facingRight)
        {
            Flip();
        }
        
    }
    private void FixedUpdate()
    {
        rigidbody_.velocity = new Vector2(speed *(inputMov+knockback),rigidbody_.velocity.y);
    }
    void Jump()
    {
        Instantiate(particlesJump, transform.position, transform.rotation);
        rigidbody_.AddForce(new Vector2(0, jumpForce));
       
    }
    void StopJump()
    {
        if (goingUp && !inGround)
        {
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, rigidbody_.velocity.y/1.5f);
        }
    }

    public void TakeDamage(Vector2 dir)
    {
        if (knockback == 0)
        {
            if (dir.x > 0)
            {
                knockback = -knockbackHorizontalForce;
            }
            else
            {
                knockback = knockbackHorizontalForce;
            }
            life -= 1;
            UIGame.instance.UpdateLife(life);
            if (life <= 0)
            {
                Die();

            }
            rigidbody_.AddForce(transform.up * knockbackVerticalForce);
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("GameOver");
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
    
}
