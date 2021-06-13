using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoving : MonoBehaviour
{
   
    [SerializeField] private Transform groundedChecker;
    [SerializeField] private float checkGroundRadius = 0.06f;
    [SerializeField] private LayerMask groundLayer;
    private float _lastTimeGrounded;
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private Transform player;
    private Animator _animator;
    private static readonly int Jumping = Animator.StringToHash("Jump");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        CheckIfGrounded();
    }
    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(groundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    public bool IsGrounded
    {
        get => _isGrounded;
        set => _isGrounded = value;
    }
}