using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocuser : MonoBehaviour
{
    public bool final;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            FindObjectOfType<CameraFollow>().objecToFollow = gameObject;
            FindObjectOfType<CameraFollow>().cameraSpeed = 4;
            GetComponent<BoxCollider2D>().enabled = false;
            if (final) AudioManager.Instance.PlaySong("ganar");
        }
    }
}
