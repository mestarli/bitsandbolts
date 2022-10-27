using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float upMaxDistance;
    Player player;
    public float cameraSpeed;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        float dist = player.transform.position.y - transform.position.y;
        if(dist > upMaxDistance)
        {
            transform.Translate(transform.up * cameraSpeed *dist* Time.deltaTime);
        }
    }
    
}
