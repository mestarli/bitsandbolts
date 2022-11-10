using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2 : Enemy
{
    bool initialized;
    Player player;
    Rigidbody2D rigidbody2d;
    public float vel;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rigidbody2d=GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 dir = new Vector2(player.transform.position.x, transform.position.y) - new Vector2(transform.position.x, transform.position.y);
        
        if(player.transform.position.x - transform.position.x > 0)
        {
            rigidbody2d.velocity = new Vector2(dir.magnitude * vel, 0);
        }
        else 
        {
            rigidbody2d.velocity = new Vector2(-dir.magnitude * vel, 0);
        }
    }
}
