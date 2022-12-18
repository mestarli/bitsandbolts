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
    SpriteRenderer spriteRenderer;

    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void TakeDmg(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            
            Die();
        }
        StartCoroutine(DamageFeedback());
    }

    public virtual void Die()
    {
        Player _player = FindObjectOfType<Player>();
        _player.GainPoints(pointsToEarn);
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

    IEnumerator DamageFeedback()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.07f);
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.07f);
    }
}
