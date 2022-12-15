using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private bool _playerOnPlatform;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.4f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _collider = other.gameObject.GetComponent<Player>().gameObject.transform.GetChild(2).GetComponent<Collider2D>();
            _playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other,true);
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other,false);
    }
}
