using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Phantom : Enemy
{
    public float range;
    public float speed;
    public float minDistance;
    public float maxDistance;
    public Player player;
    public GameObject projetile;
    Rigidbody2D rigidbody2D_;
    bool teleporting;

    public override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        try
        {
            version = UI_Manager.Instance.PhantomVersion();
        }
        catch (Exception e)
        {
            version = 2;
            Console.WriteLine(e);
        }
    }
    private void Start()
    {
        IA();
    }

    private void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        if (version == 2&& direction.magnitude<range)
        {
            if(direction.magnitude > 6&&!teleporting)
            {
                StartCoroutine(Teleport());
            }
            rigidbody2D_.velocity = direction.normalized * speed;
        }
    }
    void IA()
    {
        if (version == 1)
        {
            Vector2 dist = player.transform.position - transform.position;
            if (dist.magnitude <= range)
            {
                range = 100;
                GameObject magic = (GameObject)Instantiate(projetile, transform.position, new Quaternion(0, 0, 0, 0));
                magic.GetComponent<EnemyProjectile>().Set(dist);
                StartCoroutine(Teleport());
                AudioManager.Instance.PlaySong("bola-energia");
            }

        }
    }

    IEnumerator Teleport()
    {
        
        teleporting = true;
        Vector2 dist = player.transform.position - transform.position;
        if (dist.magnitude <= range)
        {
            yield return new WaitForSeconds(1f);
            transform.position = new Vector2(Random.Range(player.transform.position.x + minDistance, player.transform.position.x + maxDistance), Random.Range(player.transform.position.y + minDistance, player.transform.position.y + maxDistance));
            yield return new WaitForSeconds(1f);
        }
        teleporting = false;
        if (version == 1)
        {
            yield return new WaitForSeconds(2);
        }
        IA();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && version==2)
        {
            Vector2 dir = transform.position - collision.transform.position;
            collision.GetComponent<Player>().TakeDamage(dir);
        }
    }

    public override void TakeDmg(int dmg)
    {
        if (hp > 0)
        {
            AudioManager.Instance.PlaySong("golpe-fantasma");
        }
        else
        {
            AudioManager.Instance.PlaySong("muerte-fantasma");
        }
        StartCoroutine(Teleport());
        base.TakeDmg(dmg);
    }

}

