using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            transform.parent.GetComponent<Boss2>().active = true;

            transform.parent.GetComponent<Boss2>().StartCoroutine(transform.parent.GetComponent<Boss2>().IA());
            Destroy(gameObject);
        }
    }
}
