using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameObject end;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            FindObjectOfType<CameraFollow>().GetComponent<BoxCollider2D>().enabled = false;
            FindObjectOfType<CameraFollow>().objecToFollow = end;
        }
    }

    public void End() 
    { 
            SceneManager.LoadScene("Win");
    }
}
