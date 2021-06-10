using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform groundedChecker;
    [SerializeField] private float checkGroundRadius = 0.06f;
    [SerializeField] private LayerMask groundLayer;

    private bool _isGrounded;
    private Rigidbody2D _rigidbody;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
    }

    void CheckIfGrounded()
    {
        // Collider2D colliders = Physics2D.OverlapCircle(groundedChecker.position, checkGroundRadius, groundLayer);
        // if (colliders)
        // {
        //     _isGrounded = true;
        // }
        // else
        // {
        //     _isGrounded = false;
        // }
        if (Vector3.Magnitude(_rigidbody.velocity) > 0.01)
        {
            _isGrounded = false;
        }
        else
        {
            
            _isGrounded = true;
        }
        _animator.SetBool("IsGrounded",_isGrounded);
    }
}