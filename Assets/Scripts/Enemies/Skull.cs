using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : Enemy
{
    Rigidbody2D rigidbody2D_;
    Vector2 direction;
    public float horizontalSpeed;
    public float verticalSpeed;
    public Player player;

    public override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        direction = -transform.right + transform.up;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Jump()
    {
        Vector2 dir = player.transform.position - transform.position;
        if (dir.magnitude < 6)
        {
            Debug.Log(dir.magnitude);
            AudioManager.Instance.PlaySong("bote-calavera");
        }
        rigidbody2D_.velocity = new Vector2(0, 0);
        rigidbody2D_.AddForce(new Vector2(direction.normalized.x * horizontalSpeed, direction.normalized.y * verticalSpeed));
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("Floor"))
        {
            Jump();
        }
    }
}
