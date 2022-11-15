using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMisile : MonoBehaviour
{
    [SerializeField] private int type_misile;

    void Update()
    {
        
         StartCoroutine(destroyWall());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.GetComponent<Player>())
        {
            Vector2 dir = transform.position - collision.transform.position;
            collision.GetComponent<Player>().TakeDamage(dir);
            if (type_misile == 1)
            {
                Destroy(gameObject);
            }
            
        }
    }

    IEnumerator destroyWall()
    {
        yield return new WaitForSeconds(2.5f);
        if (type_misile == 2)
        {
            Destroy(gameObject);
        }
    }
}
