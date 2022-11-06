using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        if (UI_Manager.Instance)
        {
            version = UI_Manager.Instance.PhantomVersion();
        }
        else
        {
            version = 1;
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
                GameObject bone = (GameObject)Instantiate(projetile, transform.position, new Quaternion(0, 0, 0, 0));
                bone.GetComponent<EnemyProjectile>().Set(dist);
                StartCoroutine(Teleport());
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
        IA();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && version == 2 && !teleporting)
        {
            //da�o jeje
        }
    }

    public override void TakeDmg(int dmg)
    {
        StartCoroutine(Teleport());
        base.TakeDmg(dmg);
    }

}

