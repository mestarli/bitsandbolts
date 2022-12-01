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
    public Animator animator;

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
    int doubleJump;
    public float maxInmuneTime;
    float inmuneTime;

    public bool slip;

    public bool facingRight = true;
    
    [SerializeField] private int life = 3;
    [SerializeField] private GameObject ContentWeapon;
    [SerializeField] private GameObject ActiveWeapon;
    
    [SerializeField] private int positionActiveWeapon = 0;
    
    [SerializeField]private GameObject topPositionWeapons;
    
    public bool canAttack;

    public bool topAttack;
    
    [SerializeField] private Sprite player_01_model;
    [SerializeField] private Sprite player_02_model;
    // Start is called before the first frame update
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private GameObject[] weapons;
    private void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        checker = transform.GetChild(0);
        canAttack = true;
        topAttack = false;

        try
        {
            if (UI_Manager.Instance.ModelerSelection() == 1 && UI_Manager.Instance.AnimationSelection()  == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = player_01_model;
                gameObject.GetComponent<Animator>().SetLayerWeight(0,1f);
            }
            if (UI_Manager.Instance.ModelerSelection() == 2  && UI_Manager.Instance.AnimationSelection()  == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = player_02_model;
                gameObject.GetComponent<Animator>().SetLayerWeight(0,0f);
                gameObject.GetComponent<Animator>().SetLayerWeight(1,1f);
            }
            if (UI_Manager.Instance.ModelerSelection() == 1 && UI_Manager.Instance.AnimationSelection()  == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = player_01_model;
                gameObject.GetComponent<Animator>().SetLayerWeight(0,0f);
                gameObject.GetComponent<Animator>().SetLayerWeight(2,1f);
            }
            if (UI_Manager.Instance.ModelerSelection() == 2  && UI_Manager.Instance.AnimationSelection()  == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = player_02_model;
                gameObject.GetComponent<Animator>().SetLayerWeight(0,0f);
                gameObject.GetComponent<Animator>().SetLayerWeight(3,1f);
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
        if (inmuneTime > 0)
        {
            inmuneTime -= Time.deltaTime;
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && inGround || Input.GetKeyDown(KeyCode.Space) && doubleJump > 0) 
        {
            if (!inGround)
            {
                doubleJump--;
            }
            inGround = false;
            Jump();
        }
   
        if (!Input.GetKey(KeyCode.Space))
        {
            StopJump();
        }
        
        inGround = Physics2D.OverlapCircle(checker.position, radiusChecker, layerMaskGround);

        if (inGround)
        {
            doubleJump = 1;
        }

        animator.SetBool("OnGround", inGround);
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
            topAttack = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            topAttack = false;
        }

        if(positionActiveWeapon!=1 &&  topAttack && Input.GetMouseButtonDown(0) && canAttack)
        {
            animator.SetTrigger("Attack");
            canAttack = false;
            //ContentWeapon.transform.localPosition = topPositionWeapons;
            GameObject weapon = Instantiate(weapons[positionActiveWeapon], topPositionWeapons.transform.position, Quaternion.identity);
            weapon.GetComponent<Weapon>().isAttacking = true;
        }
        if(!topAttack && Input.GetMouseButtonDown(0) && canAttack)
        {
            animator.SetTrigger("Attack");
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
        if (!slip)
        {
            rigidbody_.velocity = new Vector2(speed * (inputMov + knockback), rigidbody_.velocity.y);
        }
    }
    void Jump()
    {
        rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, 0);
        animator.SetTrigger("Jump");
        AudioManager.Instance.PlaySong("jump");
        Instantiate(particlesJump, transform.position, transform.rotation);
        rigidbody_.AddForce(new Vector2(0, jumpForce));
       
    }
    void StopJump()
    {
        animator.SetTrigger("StopJump");
        if (goingUp && !inGround)
        {
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, rigidbody_.velocity.y/1.5f);
        }
    }

    public void TakeDamage(Vector2 dir)
    {
        if (inmuneTime <= 0)
        {
            if (knockback == 0)
            {
                animator.SetTrigger("Hit");

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
                if (life > 0)
                {
                    AudioManager.Instance.PlaySong("golpe-player");
                    inmuneTime = maxInmuneTime;
                }
                if (life <= 0)
                {
                    AudioManager.Instance.PlaySong("death");
                    Die();

                }
                rigidbody_.AddForce(transform.up * knockbackVerticalForce);
            }
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
