using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    float inputMov;
    Rigidbody2D rigidbody_;
    public float speed;
    public float jumpForce;
    public float gravityScale;
    float lastPosition=0;

    Transform checker;
    public float radiusChecker;
    public LayerMask layerMaskGround;
    bool inGround;
    bool goingUp;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        checker = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPosition < transform.position.y)
        {
            goingUp=true;
        }
        else
        {
            goingUp = false;
        }
        inputMov = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && inGround) 
        {
            inGround = false;
            Jump();
        }
   
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopJump();
        }
        
        inGround = Physics2D.OverlapCircle(checker.position, radiusChecker, layerMaskGround);

        lastPosition = transform.position.y;
    }
    private void FixedUpdate()
    {
        rigidbody_.velocity = new Vector2(speed *inputMov,rigidbody_.velocity.y);
    }
    void Jump()
    {
        rigidbody_.AddForce(new Vector2(0, speed * jumpForce));
       
    }
    void StopJump()
    {
        if (goingUp && !inGround)
        {
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, rigidbody_.velocity.y/1.5f);
        }
    }
}
