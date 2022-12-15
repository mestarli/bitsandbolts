using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float upMaxDistance;
    public float downMaxDistance;
    public GameObject objecToFollow;
    public float cameraSpeed;
    Player player;


    private void Awake()
    {
        objecToFollow = FindObjectOfType<Player>().gameObject;
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        float dist = objecToFollow.transform.position.y - transform.position.y;
        if((dist > upMaxDistance  || dist < -downMaxDistance) && !player.respawning)
        {
            transform.Translate(transform.up * cameraSpeed *dist* Time.deltaTime);
        }
    }
    
}
