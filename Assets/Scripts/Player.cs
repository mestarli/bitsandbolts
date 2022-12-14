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

    public static int puntuation;
    public static int puntuationMultiplier=1;

    public GameObject respawn;
    public bool respawning;
    private static int lives = 3;
    public GameObject _hitbox;

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
    
    [SerializeField] private static int life = 3;
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
    
    [SerializeField] private GameObject vidasUI;
    [SerializeField] private GameObject puntuacionUI;
    [SerializeField] private GameObject multiplicadorUI;
    
    public static Player  Instance { get; private set; }
    private void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        checker = transform.GetChild(0);
        canAttack = true;
        topAttack = false;
        _hitbox = transform.GetChild(2).gameObject;

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

    void Start(){
        multiplicadorUI.GetComponent<TextMeshProUGUI>().text = puntuationMultiplier.ToString();
        puntuacionUI.GetComponent<TextMeshProUGUI>().text = puntuation.ToString("0000000000000000");
        UIGame.instance.UpdateLifeScenes(life);
        updateVidas();

    }
// Update is called once per frame
void Update()
    {
        if (respawning)
        {
            rigidbody_.gravityScale = 0.03f;
            _hitbox.SetActive(false);
            animator.SetBool("Respawning", true);
            UIGame.instance.RestartLife();
        }
        else
        {
            rigidbody_.gravityScale = 1;
            _hitbox.SetActive(true);
            animator.SetBool("Respawning", false);
        }
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

        
        
       /* else if(Input.GetKeyDown(KeyCode.Space) && doubleJump > 0)
        {
            animator.SetTrigger("Jump_02");
        }*/
   
        if (!Input.GetKey(KeyCode.Space))
        {
            StopJump();
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            inGround = Physics2D.OverlapCircle(checker.position, radiusChecker, layerMaskGround);
        }

        if (inGround)
        {
            animator.SetBool("OnGround",true);
            doubleJump = 1;
        }

        animator.SetBool("OnGround", inGround);
        lastPosition = transform.position.y;
        
        
        //Press q for change weapon
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
            // Para evitar el delay en el ataque
            switch(positionActiveWeapon) 
            {
                case 0:
                    AudioManager.Instance.PlaySong("lanzar-hacha");
                    break;
                case 1:
                    AudioManager.Instance.PlaySong("lanzar-daga");
                    break;
                case 2:
                    AudioManager.Instance.PlaySong("lanzar-boomerang");
                    break;
            }
            canAttack = false;
            //ContentWeapon.transform.localPosition = topPositionWeapons;
            GameObject weapon = Instantiate(weapons[positionActiveWeapon], topPositionWeapons.transform.position, Quaternion.identity);
            weapon.GetComponent<Weapon>().isAttacking = true;
        }
        if(!topAttack && Input.GetMouseButtonDown(0) && canAttack)
        {
            animator.SetTrigger("Attack");
            canAttack = false;
            
            // Para evitar el delay en el ataque
            switch(positionActiveWeapon) 
            {
                case 0:
                    AudioManager.Instance.PlaySong("lanzar-hacha");
                    break;
                case 1:
                    AudioManager.Instance.PlaySong("lanzar-daga");
                    break;
                case 2:
                    AudioManager.Instance.PlaySong("lanzar-boomerang");
                    break;
            }
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

        if (rigidbody_.velocity.x != 0)
        {
            animator.SetBool("Walking",true);
        }
        else
        {
            animator.SetBool("Walking",false);
        }

    }
    public void AnimatorStopJumpFalse()
    {


        animator.SetBool("StopJump", false);
    }
    void Jump()
    {
        inGround = false;
        animator.SetBool("StopJump", false);
        animator.SetTrigger("Jump");
        respawning = false;
        rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, 0);
       
        AudioManager.Instance.PlaySong("jump");
        Instantiate(particlesJump, transform.position - new Vector3(0,0.7f,0), transform.rotation);
        rigidbody_.AddForce(new Vector2(0, jumpForce));
       
    }
    void StopJump()
    {
        if (goingUp && !inGround)
        {
            animator.SetBool("StopJump", true);
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, rigidbody_.velocity.y/1.5f);
        }
    }

    public void TakeDamage(Vector2 dir)
    {
        if (inmuneTime <= 0 && !respawning)
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
                puntuationMultiplier -= 5;
                if(puntuationMultiplier < 1)
                {
                    puntuationMultiplier = 1;
                }
                multiplicadorUI.GetComponent<TextMeshProUGUI>().text = puntuationMultiplier.ToString();
            }
        }
    }

    public void Die()
    {
        AudioManager.Instance.PlaySong("muerte-player");
        if (lives > 0)
        {
            lives--;
            vidasUI.transform.GetChild(lives).gameObject.SetActive(false);
            life = 3;
            respawning = true;
            transform.position = respawn.transform.position;
            rigidbody_.velocity = Vector2.zero;
        }
        else
        {
            lives = 3;
            life = 3;
            puntuationMultiplier = 0;
            puntuation = 0;
            SceneManager.LoadScene("GameOver");
        }
    }
    
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    public void GainPoints(int points)
    {
        puntuation += points * puntuationMultiplier;
        puntuationMultiplier++;
        multiplicadorUI.GetComponent<TextMeshProUGUI>().text = puntuationMultiplier.ToString();
        puntuacionUI.GetComponent<TextMeshProUGUI>().text = puntuation.ToString("0000000000000000");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MainCamera") && respawning)
        {
            Jump();
        }
        if(collision.CompareTag("boss"))
        { 
            Vector2 dir = transform.position - collision.transform.position;
            TakeDamage(dir);
        }
        
    }

    private void updateVidas()
    {

        Debug.Log(lives);
        switch (lives)
        {
            case 1:
                vidasUI.transform.GetChild(0).gameObject.SetActive(true);
                vidasUI.transform.GetChild(1).gameObject.SetActive(false);
                vidasUI.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 2:
                vidasUI.transform.GetChild(0).gameObject.SetActive(true);
                vidasUI.transform.GetChild(1).gameObject.SetActive(true);
                vidasUI.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 3:
                vidasUI.transform.GetChild(0).gameObject.SetActive(true);
                vidasUI.transform.GetChild(1).gameObject.SetActive(true);
                vidasUI.transform.GetChild(2).gameObject.SetActive(true);
                break;
        }

    }
}
