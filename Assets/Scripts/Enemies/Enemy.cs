using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int version;
    public GameObject explosion;
    public UI_Manager uImanager;
    private void Awake()
    {

        uImanager = FindObjectOfType<UI_Manager>();
    }
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
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().TakeDamage();
        }
    }
}
