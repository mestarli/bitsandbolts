using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public Limit limit_;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>()|| collision.GetComponent<Enemy>() || collision.GetComponent<EnemyProjectile>())
        {
            collision.transform.position = new Vector2(limit_.transform.position.x, collision.transform.position.y);
        }
    }
}
