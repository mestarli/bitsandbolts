using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int version;
    public GameObject explosion;
    public Animator animator;
    public int pointsToEarn;
    public virtual void TakeDmg(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            
            Die();
        }
    }

    public virtual void Die()
    {
        Player _player = FindObjectOfType<Player>();
        _player.gainPoints(pointsToEarn);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Vector2 dir = transform.position - collision.transform.position;
            collision.GetComponent<Player>().TakeDamage(dir);
        }
    }
}
