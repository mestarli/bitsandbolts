using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float upMaxDistance;
    public GameObject objecToFollow;
    public float cameraSpeed;


    private void Awake()
    {
        objecToFollow = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        float dist = objecToFollow.transform.position.y - transform.position.y;
        if(dist > upMaxDistance)
        {
            transform.Translate(transform.up * cameraSpeed *dist* Time.deltaTime);
        }
    }
    
}
