using System;
using System.Collections;
using System.Collections.Generic;
using Events_Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;

    [FormerlySerializedAs("collide")] [SerializeField] private VoidEvent collideEvent;
    [SerializeField] private StringEvent collideEventWithString;
    [SerializeField] private CustomClassEvent classEvent;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();

        _rb.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // if (collideEvent != null)
        // {
        //     collideEvent.RaiseEvent();
        // }

        if (collideEventWithString != null)
        {
            collideEventWithString.RaiseEvent(col.gameObject.name);
        }

        if (classEvent != null && col.gameObject.CompareTag("Enemy"))
        {
            classEvent.RaiseEvent(new DataClass("Enemy", 26, 100, 20));
        }
    }
}
