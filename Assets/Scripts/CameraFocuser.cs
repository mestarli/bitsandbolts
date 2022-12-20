using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocuser : MonoBehaviour
{
    public bool final;
    public bool boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            FindObjectOfType<CameraFollow>().objecToFollow = gameObject;
            FindObjectOfType<CameraFollow>().cameraSpeed = 4;
            GetComponent<BoxCollider2D>().enabled = false;
            if (final) AudioManager.Instance.PlaySong("ganar");
            if (boss)
            {
                AudioManager.Instance.PlaySong("boss_01");
                AudioManager.Instance.StopSong("bg_level_01"); ;
            }
        }
    }
}
