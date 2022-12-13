using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2 : Enemy
{
    GameObject sword;
    GameObject swordPoint;
    bool initialized;
    Player player;
    Rigidbody2D rigidbody2d;
    public float vel;
    bool lookat;
    public List<GameObject> batSpawners;
    public GameObject Bat;
    Animator animator;
    public float speed;
    public bool active=false;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody2d=GetComponent<Rigidbody2D>();
        swordPoint = transform.GetChild(0).gameObject;
        sword = swordPoint.transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
    }

    
    

    private void Update()
    {
        if (active)
        {
            Vector2 dir = new Vector2(player.transform.position.x, transform.position.y) - new Vector2(transform.position.x, transform.position.y);

            if (player.transform.position.x - transform.position.x > 0)
            {
                rigidbody2d.velocity = new Vector2(dir.magnitude * vel, 0);
            }
            else
            {
                rigidbody2d.velocity = new Vector2(-dir.magnitude * vel, 0);
            }

            if (lookat)
            {
                Vector2 dir2 = player.transform.position - transform.position;
                sword.transform.up = dir2;
            }
        }
    }

    public IEnumerator IA()
    {
        yield return new WaitForSeconds(4);
        int dice = Random.Range(0, 5);
        if(dice <= 3)
        {
            StartThrowSword();
        }
        else
        {
            SpawnBat();
        }

        StartCoroutine(IA());
    }

    void StartThrowSword()
    {
        if (sword.transform.parent == swordPoint.transform)
        {
            lookat = true;
            animator.SetTrigger("ThrowSword");
        }
        else
        {
            StartCoroutine(ReturnSword());
        }
    }
    void ThrowSwordFix()
    {
        StartCoroutine(ThrowSwordCorroutine()); 
        sword.GetComponent<Rigidbody2D>().bodyType = UnityEngine.RigidbodyType2D.Dynamic;
    }
    IEnumerator ThrowSwordCorroutine()
    {
        lookat = false;
        sword.transform.parent = null;
        Vector3 pos = player.transform.position;
        Vector3 dir = pos - sword.transform.position;
        while (dir.magnitude > 5f)
        {
            Debug.Log(sword.GetComponent<Rigidbody2D>().velocity + " / " + dir.magnitude);
            sword.GetComponent<Rigidbody2D>().velocity = dir.normalized * speed; 
            dir = pos - sword.transform.position;
            yield return null;
        }

        sword.GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
        yield return null;
        sword.GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
        yield return null;
        sword.GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
        yield return null;
        sword.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    IEnumerator ReturnSword()
    {
        Vector3 dir = swordPoint.transform.position - sword.transform.position;
        while (dir.magnitude > 2f)
        {
            sword.GetComponent<Rigidbody2D>().velocity = dir.normalized*speed*2;
            dir = swordPoint.transform.position - sword.transform.position;
            yield return null;
        }
        sword.transform.parent = swordPoint.transform;
        sword.transform.localPosition = new Vector2(0, 0);
        sword.GetComponent<Rigidbody2D>().bodyType = UnityEngine.RigidbodyType2D.Kinematic;
    }

    void SpawnBat()
    {
        GameObject spawner = batSpawners[Random.Range(0, batSpawners.Count - 1)];
        Instantiate(Bat, spawner.transform.position, spawner.transform.rotation);
    }
}
