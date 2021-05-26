using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform groundedChecker;
    [SerializeField] private float checkGroundRadius = 0.06f;
    [SerializeField] private LayerMask groundLayer;

    private bool _isGrounded;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

        _animator.SetBool("IsGrounded",_isGrounded);
    }
}