using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject boss;
    [SerializeField] private string boss_song;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            boss.SetActive(true);
            AudioManager.Instance.PlaySong(boss_song);
            AudioManager.Instance.StopSong("bg_level_01");
            gameObject.GetComponent<ActivateBoss>().enabled = false;
        }
    }
}
