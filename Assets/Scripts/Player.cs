using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    float inputMov;
    Rigidbody2D rigidbody_;
    public float speed;
    public float jumpForce;
    public float gravityScale;

    Transform checker;
    public float radiusChecker;
    public LayerMask layerMaskGround;
    public bool inGround;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        checker = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        inputMov = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        inGround = Physics2D.OverlapCircle(checker.position, radiusChecker, layerMaskGround);
    }
    private void FixedUpdate()
    {
        rigidbody_.velocity = new Vector2(speed *inputMov,rigidbody_.velocity.y);
    }
    void Jump()
    {
        if (inGround)
        {
            rigidbody_.AddForce(new Vector2(0, speed * jumpForce));
        }
    }
}
