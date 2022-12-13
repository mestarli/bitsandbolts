using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEdge : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.transform.GetChild(2).GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(DieCall(collision.GetComponent<Player>()));
        }
    }

    IEnumerator DieCall(Player player)
    {

        yield return new WaitForSeconds(0.4f);
        player.transform.GetChild(2).GetComponent<CapsuleCollider2D>().enabled =true;
        player.Die();

    }
}
