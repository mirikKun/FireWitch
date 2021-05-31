using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjinAI : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    private float _directionalSpeed;
    [SerializeField] private float positionOfPatrol = 2;
    [SerializeField] private Transform point;
    [SerializeField] private float chaseDistance = 5;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float reloadTime = 1;
    private bool _movingRight;
    private float _currentTimer;
    private float _scaleX;

    private Transform _player;
    private Transform _transform;
    private Transform _viewTransform;
    private enum State
    {
        Patrols,
        Shoots,
        Recharges
    };


    private State _state;

    private void Start()
    {
        _state = State.Patrols;
        _transform = transform;
        _viewTransform = _transform.GetComponentInChildren<Animator>().transform;
        _directionalSpeed = -speed;
        _player = FindObjectOfType<PlayerMovement>().transform;
        _scaleX = _viewTransform.localScale.x;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.position) < chaseDistance && _state != State.Recharges)
        {
            _state = State.Shoots;
        }

        switch (_state)
        {
            case State.Patrols:
                Chill();
                break;
            case State.Shoots:
                Shooting();
                break;
            case State.Recharges:
                Reload();
                break;
        }
    }

    private void Flip(bool moveRight)
    {
        var localScale = _viewTransform.localScale;
        if (moveRight)
        {
            localScale = new Vector3(-_scaleX, localScale.y, localScale.z);
        }
        else
        {
            localScale = new Vector3(_scaleX, localScale.y, localScale.z);
        }

        _viewTransform.localScale = localScale;
    }

    private void Chill()
    {
        if (_movingRight && transform.position.x > point.position.x + positionOfPatrol)
        {
            _directionalSpeed = -speed;
            _movingRight = false;
            Flip(_movingRight);
        }
        else if (!_movingRight && transform.position.x < point.position.x - positionOfPatrol)
        {
            _directionalSpeed = speed;
            _movingRight = true;
            Flip(_movingRight);
        }

        _transform.position += _directionalSpeed * Time.deltaTime * Vector3.right;
    }

    private void Shooting()
    {
        Flip(_transform.position.x < _player.position.x);
        Instantiate(projectile, _transform.position, Quaternion.identity);
        _currentTimer = Time.time + reloadTime;
        _state = State.Recharges;
    }

    private void Reload()
    {
        if (_currentTimer > Time.time)
        {
            return;
        }

        _state = State.Patrols;
    }
}