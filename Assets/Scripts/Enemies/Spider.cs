using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spider : Enemy
{
    Rigidbody2D rigidbody2D_;
    Player player;
    public float horizontalSpeed;
    public float verticalSpeed;
    public LayerMask floor;
    bool onGround;
    public float gravityScale;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();

        rigidbody2D_ = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        try
        {
            version = UI_Manager.Instance.SpiderVersion();
        }
        catch (Exception e)
        {
            version = 1;
            Console.WriteLine(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.transform.position - transform.position;
        if (rigidbody2D_.gravityScale == 0 && !Physics2D.Raycast(transform.position, dir, dir.magnitude, floor))
        {
            rigidbody2D_.gravityScale = gravityScale;
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }
        if (rigidbody2D_.gravityScale != 0 && version == 2 && onGround)
        {
            rigidbody2D_.velocity = new Vector2( dir.normalized.x * horizontalSpeed/55, rigidbody2D_.velocity.y);
        }
        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-4.184f, transform.localScale.y, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector3(4.184f, transform.localScale.y, transform.localScale.y);
        }
    }
    void Jump()
    {
        Vector2 dir = player.transform.position - transform.position;
        if (dir.magnitude < 6)
        {
            AudioManager.Instance.PlaySong("salto-aranya");
        }
        animator.SetTrigger("Jump");
        rigidbody2D_.velocity = new Vector2(0, 0);
        Vector2 direction = player.transform.position - transform.position;
        int coin = Random.Range(0, 10);
        if (coin <= 8)
        {
            rigidbody2D_.AddForce(new Vector2(direction.normalized.x * horizontalSpeed, verticalSpeed));
        }
        else
        {
            rigidbody2D_.AddForce(new Vector2(-direction.normalized.x * horizontalSpeed, verticalSpeed));
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Floor") && rigidbody2D_.gravityScale != 0)
        {
            if (version == 1)
            {
                Jump();
            }
            else if (version == 2)
            {
                onGround = true;
            }
        }
    }
    public override void TakeDmg(int dmg)
    {
        if (hp > 0)
        {
            AudioManager.Instance.PlaySong("golpe-aranya");
        }
        else
        {
            AudioManager.Instance.PlaySong("muerte-aranya");
        }
        base.TakeDmg(dmg);
    }
}
