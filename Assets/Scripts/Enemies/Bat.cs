using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public float speed;
    public float speedV;
    Vector2 direction = new Vector2(1, 1);
    Rigidbody2D rigidbody2D_;
    public float driver;
    public float driverX;
    bool driverXActivated;
    private void Awake()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        try
        {
            version = UI_Manager.Instance.SpiderVersion();
        }
        catch (Exception e)
        {
            version = 2;
            Console.WriteLine(e);
        }
        
    }
    private void Start()
    {
        StartCoroutine(ChangeDirectionY(-1));
    }
    private void Update()
    {
        
        if (version == 1)
        {
            direction = new Vector2(speed, driver * speedV);
        }
        else if (version == 2)
        {
            direction = new Vector2((driverX * speedV)+speed/2, driver * speedV);

        }
        rigidbody2D_.velocity = direction;
    }

    IEnumerator ChangeDirectionY(float desiredDirection)
    {
        if (desiredDirection < 0)
        {
            while (driver > desiredDirection)
            {
                driver -= 2 * Time.deltaTime;
                yield return null;
            }
            if (!driverXActivated)
            {
                StartCoroutine(ChangeDirectionX(-1));
                driverXActivated = true;
            }
            StartCoroutine(ChangeDirectionY(1));
        }
        else
        {
            while (driver < desiredDirection)
            {
                driver += 2  * Time.deltaTime; 
                yield return null;
            }
            StartCoroutine(ChangeDirectionY(-1));

        }
    }
    IEnumerator ChangeDirectionX(float desiredDirection)
    {
        if (desiredDirection < 0)
        {
            while (driverX > desiredDirection)
            {
                driverX -= 2 * Time.deltaTime;
                yield return null;
            }
            StartCoroutine(ChangeDirectionX(1));
        }
        else
        {
            while (driverX < desiredDirection)
            {
                driverX += 2 * Time.deltaTime;
                yield return null;
            }
            StartCoroutine(ChangeDirectionX(-1));

        }
    }
    public override void TakeDmg(int dmg)
    {
        if (hp > 0)
        {
            AudioManager.Instance.PlaySong("golpe-murcielago");
        }
        else
        {
            AudioManager.Instance.PlaySong("muerte-murcielago");
        }
        base.TakeDmg(dmg);
    }
}
