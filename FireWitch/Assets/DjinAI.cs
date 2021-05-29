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
    [SerializeField] private GameObject projectile ;
    private bool _movingRight;
 

    private Transform _player;
    private Transform _transform;

    private enum State {Chill, Angry, GoBack};

    
    private State _state;

    private void Start()
    {
        _state = State.GoBack;
        _transform = transform;
        _directionalSpeed = -speed;
        _player = FindObjectOfType<PlayerMovement>().transform;
        
    }

    private void Update()
    {
        
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && _state==State.Angry)
        {
            _state = State.Chill;
        }

        if (Vector2.Distance(transform.position, _player.position) < chaseDistance)
        {
            _state = State.Angry;
        }

        if (Vector2.Distance(transform.position, _player.position) > chaseDistance)
        {
            _state = State.GoBack;
        }

        
        switch (_state)
        {
            case State.Chill:
                Chill();
                break;
            case State.Angry:
                Angry();
                break;
            case State.GoBack:
                GoBack();
                break;
        }
    }

    private void Flip()
    {
        var localScale = _transform.localScale;
        localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        _transform.localScale = localScale;
    }
    private void Chill()
    {
        if (_movingRight&& transform.position.x > point.position.x + positionOfPatrol)
        {
            _directionalSpeed = -speed;
            _movingRight = false;
            Flip();
        }
        else if (!_movingRight && transform.position.x < point.position.x - positionOfPatrol)
        {
            _directionalSpeed = speed;
            _movingRight = true;
            Flip();
        }

        _transform.position += _directionalSpeed * Time.deltaTime*Vector3.right;
    }

    private void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
    }

    private void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
}