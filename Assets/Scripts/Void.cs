using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DieCall(collision.GetComponent<Player>()));
        }
    }

    IEnumerator DieCall(Player player)
    {

        yield return new WaitForSeconds(0.4f);
        player.Die();

    }
}
