using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float speed = 6;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f; 
    [SerializeField] private Transform groundedChecker;
    [SerializeField] private float checkGroundRadius = 0.06f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject sparks;

    private Rigidbody2D _rb;
    private Transform _transform;
    private Animator _animator;
    private bool _isGrounded;
    private bool _canDoubleJump;

    private Vector3 _localScale;
    [SerializeField] private float rememberGroundedFor = 0.1f;
    private float _lastTimeGrounded;
    private float _xMove;
    private bool _facingRight;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _transform = transform;
        _localScale = _transform.localScale;
    }

    private void Update()
    {
        Move();
        Rotate();
        Jump();
        CheckIfGrounded();
    }

    private void Move()
    {
        _xMove = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat(Speed, Mathf.Abs(_xMove));
        float moveBy = _xMove * speed;
        _rb.velocity = new Vector2(moveBy, _rb.velocity.y);
    }

    private void Rotate()
    {
        if (_xMove == 0)
            return;

        _facingRight = _xMove > 0;
        if (!Input.GetButton("Fire1") && ((_facingRight && _localScale.x > 0) || (!_facingRight && _localScale.x < 0)))
        {
            Flip();
        }
    }

    private void Flip()
    {
        _localScale.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void RotateToFire(Vector2 mousePosition)
    {
        if ((_transform.position.x < mousePosition.x && _localScale.x > 0) ||
            (_transform.position.x > mousePosition.x && _localScale.x < 0))
        {
            Flip();
        }
    }

    private void Jump()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * ((fallMultiplier - 1) * Time.deltaTime);
        }
        else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * ((lowJumpMultiplier - 1) * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.Space) &&
            (_isGrounded || Time.time - _lastTimeGrounded <= rememberGroundedFor || _canDoubleJump))
        {
            _canDoubleJump = false;
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            if (!_isGrounded)
            {
                Destroy(Instantiate(sparks, groundedChecker.position, Quaternion.identity, _transform), 3);
            }
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(groundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders)
        {
            _isGrounded = true;
            _canDoubleJump = true;
        }
        else
        {
            if (_isGrounded)
            {
                _lastTimeGrounded = Time.time;
            }

            _isGrounded = false;
        }

        _animator.SetBool(IsJumping, !_isGrounded);
    }
}