using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Enemy
{
    Rigidbody2D rigidbody2D_;
    Player player;
    public float horizontalSpeed;
    public float verticalSpeed;
    private void Awake()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }
        // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
    void Jump()
    {
        rigidbody2D_.velocity = new Vector2(0, 0);
        Vector2 direction = player.transform.position - transform.position;
        rigidbody2D_.AddForce(new Vector2(direction.normalized.x * horizontalSpeed, verticalSpeed));
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>()&&rigidbody2D_.gravityScale==0)
        {
            collision.GetComponent<Player>().TakeDamage();
            rigidbody2D_.gravityScale = 1;
            Jump();
        }
        if (collision.gameObject.CompareTag("Floor") && rigidbody2D_.gravityScale!=0)
        {
            Jump();
        }
    }

}
