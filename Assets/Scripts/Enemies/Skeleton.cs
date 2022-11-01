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

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        version = UI_Manager.Instance.SkeletonVersion();
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
            rigidbody2D_.velocity = transform.right * speedRoam;
        }
    }

    void IA()
    {
        if (version == 1)
        {
            Vector2 dist = player.transform.position - transform.position;
            if(dist.magnitude <= range)
            {
                GameObject bone = (GameObject)Instantiate(boneProjectile, transform.position, new Quaternion(0,0,0,0));
                bone.GetComponent<EnemyProjectile>().Set(dist);
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

    IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
        IA();
    }
}
