using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : EnemyProjectile
{
    public float speedUp;
    public override void Update()
    {
        
    }
    private void Start()
    {
        
        _rigidbody2d().AddForce(transform.up.normalized * speedUp,ForceMode2D.Impulse);
        _rigidbody2d().AddForce(Direction().normalized * speed,ForceMode2D.Impulse);
    }
}
