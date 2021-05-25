using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private GameObject sparks;
    [SerializeField] private float dashTime;
    private Transform _transform;
    private float _curDashTime;
    private Rigidbody2D _rb;
    private bool _isDashing;

    void Start()
    {
        _transform = transform;
        _curDashTime = dashTime;
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDashing)
        {
            StartCoroutine(Dash());
            Destroy(Instantiate(sparks, _transform.position, Quaternion.Euler(_transform.forward), _transform), 3);
        }
    }

    private IEnumerator Dash()
    {
        Vector2 dashDirection = _transform.right;
        _isDashing = true;
        while (_curDashTime > 0)
        {
            _curDashTime -= Time.deltaTime;
            _rb.velocity = dashDirection * -dashSpeed;
            yield return null;
        }
        _curDashTime = dashTime;
        _isDashing = false;
        _rb.velocity = Vector2.zero;
    }
}