using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slipper : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            transform.parent.GetComponent<Player>().slip = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            transform.parent.GetComponent<Player>().slip = false; 
        }
    }
}
