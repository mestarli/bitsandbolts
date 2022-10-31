using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public bool ignoresWalls;
    Vector2 direction;
    Rigidbody2D rigidbody2D_;

    private void Awake()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();
    }

    public void Set(Vector2 direction_)
    {
        direction = direction_;
    }

    private void Update()
    {
        rigidbody2D_.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Vector2 dir = transform.position - collision.transform.position;
            collision.GetComponent<Player>().TakeDamage(dir);
            Destroy(gameObject);
        }
        else if (!ignoresWalls && collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
