﻿using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 50;
    [SerializeField] private float nextWayPointDistance = 3f;
    private Path _path;

    private int _currentWayPoint;
    [SerializeField] private Transform ghostGFX;

    private Seeker _seeker;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        StartCoroutine(FindWay());
    }

    private IEnumerator FindWay()
    {
        while (true)
        {
            if (_seeker.IsDone())
                _seeker.StartPath(_rb.position, target.position, OnPathCompleted);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnPathCompleted(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_path == null)
            return;
        if (_currentWayPoint >= _path.vectorPath.Count)
        {
            return;
        }
      

        Vector2 direction = ((Vector2) (_path.vectorPath[_currentWayPoint]) - _rb.position).normalized;
        Vector2 force =   speed * Time.deltaTime*direction;
        _rb.AddForce(force);
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWayPoint]);
        if (distance < nextWayPointDistance)
        {
            _currentWayPoint++;
        }

        if ((force.x >= 0.01f && ghostGFX.localScale.x > 0) ||
            (force.x <= -0.01f &&ghostGFX.localScale.x < 0))
        {
            var localScale = ghostGFX.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            ghostGFX.localScale = localScale;
        }
    }
}