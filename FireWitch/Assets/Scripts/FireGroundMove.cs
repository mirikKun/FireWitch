using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGroundMove : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private float lifeTime = 3;
    private Rigidbody2D _rb;
    private Vector3 _localScale;

    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {   
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifeTime);
    }

    private void Update()
    {
        _rb.velocity = new Vector2(-speed, _rb.velocity.y);
    }

    public void ChangeDirection()
    {
        _localScale = _transform.localScale;
        _localScale.x *= -1;
        _transform.localScale = _localScale;
        speed *= -1;
    }
}
