using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    bool roam;
    public float range;
    public float speedRoam;
    public Player player;
    public GameObject boneProjectile;
    public GameObject skull;
    Rigidbody2D rigidbody2D_;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        try
        {
            version = UI_Manager.Instance.SkeletonVersion();
        }
        catch (Exception e)
        {
            version = 1;
            Console.WriteLine(e);
        }
    }
    void Start()
    {
        StartCoroutine(waitTime(1));
    }

    // Update is called once per frame
    void Update()
    {
        if (roam)
        {
            rigidbody2D_.velocity = new Vector2( transform.right.x * speedRoam, rigidbody2D_.velocity.y);
            animator.SetBool("HeadLess",true);
        }
    }

    void IA()
    {
        if (version == 1)
        {
            Vector2 dist = player.transform.position - transform.position;
            if(dist.magnitude <= range && transform.position.y >= player.transform.position.y)
            {
                animator.SetTrigger("Shoot");
            }
            StartCoroutine(waitTime(3));
        }
        else if(version ==2 && !roam)
        {
            Vector2 dist = player.transform.position - transform.position;
            if (dist.magnitude <= range)
            {
                Instantiate(skull, transform.position, new Quaternion(0, 0, 0, 0));
                roam = true;
            }
            StartCoroutine(waitTime(1));
        }
    }
    public void ShootBone()
    {
        Vector2 dist = player.transform.position - transform.position;
        GameObject bone = (GameObject)Instantiate(boneProjectile, transform.position, new Quaternion(0, 0, 0, 0));
        bone.GetComponent<EnemyProjectile>().Set(dist);
    }
    IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
        IA();
    }
    
    public override void TakeDmg(int dmg)
    {
        if (hp > 0)
        {
            AudioManager.Instance.PlaySong("golpe-skeleto");
        }
        else
        {
            AudioManager.Instance.PlaySong("muerte-skeleto");
        }
        base.TakeDmg(dmg);
    }
}
