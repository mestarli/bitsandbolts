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

    [SerializeField] private int life = 3;
    [SerializeField] private GameObject ContentWeapon;
    [SerializeField] private GameObject ActiveWeapon;
    
    [SerializeField] private int positionActiveWeapon = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        checker = transform.GetChild(0);
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            ContentWeapon.transform.GetChild(positionActiveWeapon).gameObject.SetActive(false);
            if (positionActiveWeapon < 2)
            {
                positionActiveWeapon += 1;
            }
            else
            {
                positionActiveWeapon = 0;
            }
           
            ContentWeapon.transform.GetChild(positionActiveWeapon).gameObject.SetActive(true);

            Debug.Log("Posicion "+positionActiveWeapon);
            UIGame.instance.UpdateWeapon(positionActiveWeapon);
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
}
